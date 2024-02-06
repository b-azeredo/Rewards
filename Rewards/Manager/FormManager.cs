using Rewards.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rewards.Manager
{
    public class FormManager
    {
        public static int Get_Activity_Id(int formId)
        {
            using (var entities = new Entities2())
            {
                var form = entities.FORM.FirstOrDefault(f  => f.ID == formId);
                return form.ACTIVITY_ID;
            }
        }

        public static int Get_User_Id(int formId)
        {
            using (var entities = new Entities2())
            {
                var form = entities.FORM.FirstOrDefault(f => f.ID == formId);
                return form.USER_ID;
            }
        }

        public static string Get_Description(int formId)
        {
            using (var entities = new Entities2())
            {
                var form = entities.FORM.FirstOrDefault(f => f.ID == formId);
                return form.DESCRIPTION;
            }
        }

        public static List<FILE> Get_Files(int formId)
        {
            using (var context = new Entities2())
            {
                return context.FILE.Where(f => f.FORM_ID == formId).ToList();
            }
        }
    }
}