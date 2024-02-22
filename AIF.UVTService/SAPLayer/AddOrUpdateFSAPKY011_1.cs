﻿using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY011_1
    {
        public Response addOrUpdateFSAPKY011_1(FSAPKY011_1 fSAPKY011_1, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            int clnum = 0;
            string dbCode = "";
            try
            {
                ConnectionList connection = new ConnectionList();

                LoginCompany log = new LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName,ID);

                if (connection.number == -1)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select * from \"@AIF_FSAPKY011_1\" where \"U_PartiNo\" = '" + fSAPKY011_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY011_1_1;

                    GeneralDataCollection oChildren_FSAPKY011_1_1;

                    GeneralData oChild_FSAPKY011_1_2;

                    GeneralDataCollection oChildren_FSAPKY011_1_2;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY011_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY011_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY011_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY011_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY011_1.Aciklama.ToString());

                    if (fSAPKY011_1.Tarih != null && fSAPKY011_1.Tarih != "")
                    {
                        string tarih = fSAPKY011_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY011_1_1 = oGeneralData.Child("AIF_FSAPKY011_1_1");

                    foreach (var item in fSAPKY011_1.fSAPKY011_1_1s)
                    {
                        oChild_FSAPKY011_1_1 = oChildren_FSAPKY011_1_1.Add();

                        oChild_FSAPKY011_1_1.SetProperty("U_KullKrema", item.KullanilanKrema);

                        oChild_FSAPKY011_1_1.SetProperty("U_KullSutMik", item.KullanilanSutMiktari);

                        oChild_FSAPKY011_1_1.SetProperty("U_KreSonYag", item.KremaSonYagi);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastBasSa", item.PastorizasyonBaslangicSaati);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastSicak", item.PastrizasyonSicakligi);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastBitSa", item.PastorizasyonBitisSaati);

                        oChild_FSAPKY011_1_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }


                    oChildren_FSAPKY011_1_2 = oGeneralData.Child("AIF_FSAPKY011_1_2");

                    foreach (var item in fSAPKY011_1.fSAPKY011_1_2s)
                    {
                        oChild_FSAPKY011_1_2 = oChildren_FSAPKY011_1_2.Add();

                        oChild_FSAPKY011_1_2.SetProperty("U_KremaPH", item.KremaPH);

                        oChild_FSAPKY011_1_2.SetProperty("U_YagOrani", item.KremaYagOrani);

                        oChild_FSAPKY011_1_2.SetProperty("U_SonYagOr", item.KremaSonYagOrani);

                        oChild_FSAPKY011_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi); 
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY011_1\"");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    { 
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Analiz girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY011_1_1;

                    GeneralDataCollection oChildren_FSAPKY011_1_1;

                    GeneralData oChild_FSAPKY011_1_2;

                    GeneralDataCollection oChildren_FSAPKY011_1_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY011_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY011_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY011_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY011_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY011_1.Aciklama.ToString());

                    if (fSAPKY011_1.Tarih != null && fSAPKY011_1.Tarih != "")
                    {
                        string tarih = fSAPKY011_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY011_1_1 = oGeneralData.Child("AIF_FSAPKY011_1_1");

                    if (oChildren_FSAPKY011_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY011_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY011_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY011_1.fSAPKY011_1_1s)
                    {
                        oChild_FSAPKY011_1_1 = oChildren_FSAPKY011_1_1.Add();

                        oChild_FSAPKY011_1_1.SetProperty("U_KullKrema", item.KullanilanKrema);

                        oChild_FSAPKY011_1_1.SetProperty("U_KullSutMik", item.KullanilanSutMiktari);

                        oChild_FSAPKY011_1_1.SetProperty("U_KreSonYag", item.KremaSonYagi);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastBasSa", item.PastorizasyonBaslangicSaati);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastSicak", item.PastrizasyonSicakligi);

                        oChild_FSAPKY011_1_1.SetProperty("U_PastBitSa", item.PastorizasyonBitisSaati);

                        oChild_FSAPKY011_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_FSAPKY011_1_2 = oGeneralData.Child("AIF_FSAPKY011_1_2");

                    if (oChildren_FSAPKY011_1_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY011_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY011_1_2.Remove(0);
                    }

                    foreach (var item in fSAPKY011_1.fSAPKY011_1_2s)
                    {
                        oChild_FSAPKY011_1_2 = oChildren_FSAPKY011_1_2.Add();

                        oChild_FSAPKY011_1_2.SetProperty("U_KremaPH", item.KremaPH);

                        oChild_FSAPKY011_1_2.SetProperty("U_YagOrani", item.KremaYagOrani);

                        oChild_FSAPKY011_1_2.SetProperty("U_SonYagOr", item.KremaSonYagOrani);

                        oChild_FSAPKY011_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }



                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = -9000, Description = "Bilinmeyen Hata oluştu. " + ex.Message, List = null };
            }

            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}