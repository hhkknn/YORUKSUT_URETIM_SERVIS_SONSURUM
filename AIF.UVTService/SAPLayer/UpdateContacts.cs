using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using NLog;

namespace UVTService.SAPLayer
{
    public class UpdateContacts
    {
        public Response updateContacts(Models.Contacts contacts, string dbName, string mKodValue)
        {
            int clnum = 0;
            string dbCode = "";

            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);
            Logger logger = LogManager.GetCurrentClassLogger();

            //var requestJson_New = JsonConvert.SerializeObject(protocol);

            //logger.Info(" ");

            logger.Info("ID: " + ID+ " updateContacts Servisine Geldi.");
            //logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

            try
            {
                //commit
                ConnectionList connection = new ConnectionList();

                SAPLayer.LoginCompany log = new SAPLayer.LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName,ID);

                if (connection.number == -1)
                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                SAPbobsCOM.Company oCompany = connection.oCompany;

                logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);

                SAPbobsCOM.CompanyService companyService = null;
                SAPbobsCOM.ActivitiesService activitiesService = null;

                companyService = oCompany.GetCompanyService();
                activitiesService = (SAPbobsCOM.ActivitiesService)companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.ActivitiesService);

                ActivityParams oParams = (ActivityParams)activitiesService.GetDataInterface(ActivitiesServiceDataInterfaces.asActivityParams);
                oParams.ActivityCode = Convert.ToInt32(contacts.ClgCode);
                Activity oGet = activitiesService.GetActivity(oParams);

                oGet.EndTime = contacts.EndTime;
                oGet.Status = Convert.ToInt32(contacts.Status);
                oGet.UserFields.Item("U_KullaniciId").Value = contacts.UserId;
                if (contacts.Closed == "Y")
                    oGet.Closed = BoYesNoEnum.tYES;

                try
                {
                    activitiesService.UpdateActivity(oGet);
                    logger.Info("ID: " + ID + " " + "Aktivite başarıyla güncellendi.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Aktivite başarıyla güncellendi.", List = null };
                }
                catch (Exception)
                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 7100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription());
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -2100, Description = "Hata Kodu - 7100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("ID: " + ID + " " + "Bilinmeyen hata oluştu. " + ex.Message);
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}