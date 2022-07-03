using Cmms.Core.Application.Exceptions;
using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Cmms.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application
{
   public interface IEquipmentService : IRepository<Equipment>
  {
   Task<Equipment> Add(AddEquipmentDto viewModel);
   Task<SparePart> AddSparePart(AddSparePartDto viewModel);

    }
    public class EquipmentService : IEquipmentService
    {
        private readonly ApplicationContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public EquipmentService(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Equipment> Add(AddEquipmentDto viewModel)
        {
            Equipment currentEq = null;
            if (viewModel.id.HasValue==true)
            {
             currentEq = await _context.Equipments.FindAsync(viewModel.id.Value);
                if (currentEq == null)
                    throw new BadRequestException("parameters is not valid");

            }
            else
            {
                currentEq = new Equipment();
                _context.Equipments.Add(currentEq);
            }
            currentEq.EquipmentName = viewModel.equipmentName;
            currentEq.CurrentStatus = viewModel.status;
           await _context.SaveChangesAsync();
            
            return currentEq;
        }


        public async Task<SparePart> AddSparePart(AddSparePartDto viewModel)
        {

            var currentEquipment = await _context.Equipments.FindAsync(viewModel.equipmentId);
            if (currentEquipment==null)
                throw new BadRequestException("equipment not found");


            SparePart part = null;
            if (viewModel.id.HasValue == true)
            {
                part = currentEquipment.SpareParts.FirstOrDefault(d => d.Id == viewModel.id.Value);

            }
            else
            {
                part = new SparePart { Id = Guid.NewGuid() };
                currentEquipment.AddSparePart(part);
            }

            if (part == null)
                throw new BadRequestException("your spart part not found");



            part.Description = viewModel.description;
            part.PartName = viewModel.partNumber;
            part.Id = viewModel.id.HasValue ? viewModel.id.Value : Guid.NewGuid();
            part.UseCount = viewModel.useCount;


           await _context.SaveChangesAsync();
            return part;



        }
    }
}
