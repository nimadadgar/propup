using Cmms.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Infrastructure.Dto
{
    public class UserInfoModelDto
    {
        public UserInfoModelDto(string UserId,string Email,string DisplayName, string FirstName,string SurName,
            string Location,string JobTitle,string AccessLevels,string Status,string MobilePhone)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.DisplayName = DisplayName;

            this.SurName = SurName;
            this.Location = Location;   
            this.JobTitle = JobTitle;
            this.Status = Status;
            this.Email= Email;
            this.MobilePhone = MobilePhone;
            this.AccessLevels = AccessLevels;

        }
        public string UserId { init; get; }
        public string DisplayName { set; get; }
        public string FirstName { set; get; }
        public string SurName { set; get; }
        public string Email { init; get; }
        public string Location { set; get; }
        public string JobTitle { set; get; }
        public string Status { set; get; }
        public string MobilePhone { set; get; }
        public string AccessLevels { set; get; }


        private static string GetKey(IDictionary<string, object> dictValues, string keyValue)
        {
            try
            {
                if (dictValues == null)
                    return "";

                var re = dictValues.ContainsKey(keyValue);
                if (re)
                {
                    var rr = dictValues[keyValue];

                }


                return dictValues.ContainsKey(keyValue) ? dictValues[keyValue].ToString() : "";
            }
            catch(Exception er)
            {
                return "";
            }
        }

        public static UserInfoModelDto ToUserClaim(Microsoft.Graph.User user,GraphService graph)
        {
            var identity = user.Identities.Where(d => d.SignInType == "emailAddress").Select(d => d.IssuerAssignedId).FirstOrDefault();

            


            return new UserInfoModelDto(user.Id, identity,user.DisplayName, user.GivenName, user.Surname,
               GetKey(user.AdditionalData, graph.LocationAttribute),
               GetKey(user.AdditionalData, graph.JobAttribute),
               GetKey(user.AdditionalData, graph.AccessLevelsAttribute),
               GetKey(user.AdditionalData, graph.StatusAttribute),user.MobilePhone);
                              

        }
    }

   
}
