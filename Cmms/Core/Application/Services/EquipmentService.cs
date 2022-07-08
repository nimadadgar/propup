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
   Task<Equipment> Create(CreateEquipmentDto viewModel);
   Task<Equipment> Update(UpdateEquipmentDto viewModel);
   Task<UpdateEquipmentDto> GetById(Guid id);
   Task<List<UpdateEquipmentDto>> Get();

        Task<SparePart> AddSparePart(AddSparePartDto viewModel); 
    Task<SparePart> UpdateSparePart(UpdateSparePartDto viewModel);

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

        public async Task<Equipment> Create(CreateEquipmentDto viewModel)
        {
            Equipment currentEq = new Equipment();
            currentEq.Description = viewModel.description;
            currentEq.EquipmentName = viewModel.equipmentName;
            currentEq.CurrentStatus = viewModel.status;
            _context.Equipments.Add(currentEq);
            await _context.SaveChangesAsync();
            return currentEq;
        }

        public async Task<Equipment> Update(UpdateEquipmentDto viewModel)
        {
            Equipment currentEq = await _context.Equipments.FindAsync(viewModel.id);
            
                if (currentEq == null)
                    throw new BadRequestException("parameters is not valid");

            currentEq.Description= viewModel.description; 
            currentEq.EquipmentName = viewModel.equipmentName;
            currentEq.CurrentStatus = viewModel.status;
            await _context.SaveChangesAsync();
            return currentEq;
        }

        public async Task<UpdateEquipmentDto> GetById(Guid id)
        {
            Equipment currentEq = await _context.Equipments.FindAsync(id);
            if (currentEq == null)
                throw new BadRequestException("parameters is not valid");
            return new UpdateEquipmentDto {id=id, description = currentEq.Description, equipmentName = currentEq.EquipmentName, status = currentEq.CurrentStatus };



        }
        public async Task<List<UpdateEquipmentDto>> Get()
        {

           var eq=await _context.Equipments.AsNoTracking().OrderBy(d=>d.EquipmentName).Skip(1).Take(2).ToListAsync();


            var list = await _context.Equipments.AsNoTracking().OrderBy(d => d.EquipmentName)
                .Skip(1).Take(2).Select(p => new UpdateEquipmentDto
                {
                    id = p.Id,
                    description = p.Description,
                    equipmentName = p.EquipmentName,
                    status = p.CurrentStatus
                }).ToListAsync();


            return list;


        }


        public async Task<SparePart> AddSparePart(AddSparePartDto viewModel)
        {

            var currentEquipment = await _context.Equipments.FindAsync(viewModel.equipmentId);
            if (currentEquipment==null)
                throw new BadRequestException("equipment not found");


            SparePart part = new SparePart { Id = Guid.NewGuid() };
            part.Description = viewModel.description;
            part.PartName = viewModel.partNumber;
            part.UseCount = viewModel.useCount;
            currentEquipment.AddSparePart(part);
            await _context.SaveChangesAsync();
            return part;



        }

        public async Task<SparePart> UpdateSparePart(UpdateSparePartDto viewModel)
        {

           var eq= await _context.Equipments.Where(d => d.Id == viewModel.equipmentId).AsNoTracking().FirstOrDefaultAsync();
       
            if (eq==null)
            {
                throw new BadRequestException("Equipment Not Found");
            }

           var part= eq.SpareParts.Where(d => d.Id == viewModel.id).FirstOrDefault();
            if (part==null)
                throw new BadRequestException("Your SpartPart  Not Found");



          part.Description = viewModel.description;
            part.PartName = viewModel.partNumber;
           part.UseCount = viewModel.useCount;
           await _context.SaveChangesAsync();
           return part;

        }

    }
}
