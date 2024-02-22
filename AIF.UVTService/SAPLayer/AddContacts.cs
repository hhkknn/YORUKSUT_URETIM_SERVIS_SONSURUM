using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using Newtonsoft.Json;
using NLog;

namespace UVTService.SAPLayer
{
    public class AddContacts
    {
        //commit
        public Response addContacts(Contacts contacts, string dbName, string mKodValue)
        {
            int clnum = 0;
            string companyCodeDb = "";

            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);
            Logger logger = LogManager.GetCurrentClassLogger();

            //var requestJson_New = JsonConvert.SerializeObject(protocol);

            //logger.Info(" ");

            logger.Info("ID: " + ID + " addContacts Servisine Geldi.");
            //logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

            try
            {
                ConnectionList connection = new ConnectionList();

                SAPLayer.LoginCompany log = new SAPLayer.LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName, ID);

                ////var json = JsonConvert.SerializeObject(LoginCompany.Connlist);

                ////return new Response { Value = -3100, Description = "Json" + json, List = null };

                //if (connection.number == -1)
                //{
                //    for (int i = 1; i <= 3; i++)
                //    {
                //        connection = log.getSAPConnection(dbName,ID);

                //        if (connection.number > -1)
                //        {
                //            break;
                //        }
                //    }
                //}

                if (connection.number == -1)
                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                companyCodeDb = connection.dbCode;

                SAPbobsCOM.Company oCompany = connection.oCompany;

                logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);

                SAPbobsCOM.CompanyService companyService = null;
                SAPbobsCOM.ActivitiesService activitiesService = null;
                SAPbobsCOM.Activity activity = null;

                companyService = oCompany.GetCompanyService();
                activitiesService = (SAPbobsCOM.ActivitiesService)companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.ActivitiesService);

                activity = (SAPbobsCOM.Activity)activitiesService.GetDataInterface(SAPbobsCOM.ActivitiesServiceDataInterfaces.asActivity);

                activity.HandledByEmployee = Convert.ToInt32(contacts.ContactId);
                activity.Activity = (SAPbobsCOM.BoActivities)Convert.ToInt32(contacts.ContactType);
                activity.ActivityType = Convert.ToInt32(contacts.ContactSubType);
                activity.StartDate = Convert.ToDateTime(new DateTime(Convert.ToInt32(contacts.StartDate.Substring(0, 4)), Convert.ToInt32(contacts.StartDate.Substring(4, 2)), Convert.ToInt32(contacts.StartDate.Substring(6, 2))));
                activity.StartTime = contacts.StartTime;
                activity.Status = Convert.ToInt32(contacts.Status);
                activity.Personalflag = SAPbobsCOM.BoYesNoEnum.tYES;
                activity.UserFields.Item("U_RotaCode").Value = contacts.RotaKodu;
                activity.UserFields.Item("U_PartiNo").Value = contacts.PartiNo;
                activity.UserFields.Item("U_KullaniciId").Value = contacts.UserId;

                var aa = activitiesService.AddActivity(activity);

                if (aa.ActivityCode != 0)
                {
                    logger.Info("ID: " + ID + " " + "Üretim için çıkış başarıyla oluşturuldu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = 0, Description = "Aktivite başarıyla oluşturuldu.", List = null };
                }
                else

                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 8100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription());
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -2100, Description = "Hata Kodu - 8100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("ID: " + ID + " " + "Bilinmeyen hata oluştu. " + ex.Message);
                LoginCompany.ReleaseConnection(clnum, companyCodeDb, ID);
                return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, companyCodeDb, ID);
            }
        }
    }
}