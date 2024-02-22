﻿using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY003_1
    {
        public Response addOrUpdateFSAPKY003_1(FSAPKY003_1 fSAPKY003_1, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY003_1\" where \"U_PartiNo\" = '" + fSAPKY003_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY003_1_1;

                    GeneralDataCollection oChildren_FSAPKY003_1_1;

                    GeneralData oChild_FSAPKY003_1_2;

                    GeneralDataCollection oChildren_FSAPKY003_1_2;

                    GeneralData oChild_FSAPKY003_1_3;

                    GeneralDataCollection oChildren_FSAPKY003_1_3;

                    //GeneralData oChild_FSAPKY003_2_1;

                    //GeneralDataCollection oChildren_FSAPKY003_2_1;

                    //GeneralData oChild_FSAPKY003_2_2;

                    //GeneralDataCollection oChildren_FSAPKY003_2_2;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY003_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY003_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY003_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY003_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY003_1.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY003_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY003_1.Kontrol2.ToString());

                    if (fSAPKY003_1.Tarih != null && fSAPKY003_1.Tarih != "")
                    {
                        string tarih = fSAPKY003_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY003_1_1 = oGeneralData.Child("AIF_FSAPKY003_1_1");

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_1s)
                    {
                        oChild_FSAPKY003_1_1 = oChildren_FSAPKY003_1_1.Add();
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_UniteSic", item.UniteSicakligi);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_TekneNo", item.TekneNumarasi);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_UrtUrunAdi", item.UretilenUrunAdi);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_AlSutMik", item.AlinanSutMiktari);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_TekSutSic", item.TekneSutuSicakligi);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_MayaSaat", item.MayalamaSaati);
                                       
                        oChild_FSAPKY003_1_1.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_BaskBasSaat", item.BaskilamaBasSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_BaskSonSaat", item.BaskilamaSonSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_KesmeSaat", item.KesmeSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_KesmePH", item.KesmePH);

                        oChild_FSAPKY003_1_1.SetProperty("U_SalIlvSaat", item.SalamuraIlaveSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_PeyTopSaat", item.PeynirTopSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_PeyTopPH", item.PeynirTopPH);

                        oChild_FSAPKY003_1_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }

                    oChildren_FSAPKY003_1_2 = oGeneralData.Child("AIF_FSAPKY003_1_2");

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_2s)
                    {
                        oChild_FSAPKY003_1_2 = oChildren_FSAPKY003_1_2.Add();

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalPH", item.TekneSalamuraPH);

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalBol", item.TekneSalamuraBolme);

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalSic", item.TekneSalamuraSicakligi); 
                    }

                    oChildren_FSAPKY003_1_3 = oGeneralData.Child("AIF_FSAPKY003_1_3");

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_3s)
                    {
                        oChild_FSAPKY003_1_3 = oChildren_FSAPKY003_1_3.Add();

                        oChild_FSAPKY003_1_3.SetProperty("U_NumuneNo", item.NumuneNumarasi);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutPH", item.BeyazPeynirSutPH);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutSH", item.BeyazPeynirSutSH);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutKM", item.BeyazPeynirSutKM); 

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutYag", item.BeyazPeynirSutYagi); 

                        oChild_FSAPKY003_1_3.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    //oChildren_FSAPKY003_2_1 = oGeneralData.Child("AIF_FSAPKY003_2_1");

                    //foreach (var item in fSAPKY003_1.fSAPKY003_2_1s)
                    //{
                    //    oChild_FSAPKY003_2_1 = oChildren_FSAPKY003_2_1.Add();
                                         
                    //    oChild_FSAPKY003_2_1.SetProperty("U_KapatmaPH", item.KapatmaPH);
                                         
                    //    oChild_FSAPKY003_2_1.SetProperty("U_UretimTar", item.UretimTarihi);
                                         
                    //    oChild_FSAPKY003_2_1.SetProperty("U_SonTukTar", item.SonTuketimTarihi);
                                         
                    //    oChild_FSAPKY003_2_1.SetProperty("U_PartiNo", item.PartiNumarasi);
                                         
                    //    oChild_FSAPKY003_2_1.SetProperty("U_DolSalPH", item.DolumSalamuraPH);

                    //    oChild_FSAPKY003_2_1.SetProperty("U_DolSalBol", item.DolumSalamuraBolme);

                    //    oChild_FSAPKY003_2_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    //}

                    //oChildren_FSAPKY003_2_2 = oGeneralData.Child("AIF_FSAPKY003_2_2");

                    //foreach (var item in fSAPKY003_1.fSAPKY003_2_2s)
                    //{
                    //    oChild_FSAPKY003_2_2 = oChildren_FSAPKY003_2_2.Add();

                    //    oChild_FSAPKY003_2_2.SetProperty("U_Ornek1", item.Ornek1);

                    //    oChild_FSAPKY003_2_2.SetProperty("U_Ornek2", item.Ornek2);

                    //    oChild_FSAPKY003_2_2.SetProperty("U_Ornek3", item.Ornek3);

                    //    oChild_FSAPKY003_2_2.SetProperty("U_Ornek4", item.Ornek4);

                    //    oChild_FSAPKY003_2_2.SetProperty("U_Ornek5", item.Ornek5);
                    //}


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY003_1\"");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    {
                        //if (oCompany.InTransaction)
                        //{
                        //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        //}
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

                    GeneralData oChild_FSAPKY003_1_1;

                    GeneralDataCollection oChildren_FSAPKY003_1_1;

                    GeneralData oChild_FSAPKY003_1_2;

                    GeneralDataCollection oChildren_FSAPKY003_1_2;

                    GeneralData oChild_FSAPKY003_1_3;

                    GeneralDataCollection oChildren_FSAPKY003_1_3;

                    //GeneralData oChild_FSAPKY003_2_1;

                    //GeneralDataCollection oChildren_FSAPKY003_2_1;

                    //GeneralData oChild_FSAPKY003_2_2;

                    //GeneralDataCollection oChildren_FSAPKY003_2_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY003_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY003_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY003_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY003_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY003_1.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY003_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY003_1.Kontrol2.ToString());

                    if (fSAPKY003_1.Tarih != null && fSAPKY003_1.Tarih != "")
                    {
                        string tarih = fSAPKY003_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY003_1_1 = oGeneralData.Child("AIF_FSAPKY003_1_1");

                    if (oChildren_FSAPKY003_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY003_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY003_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_1s)
                    {
                        oChild_FSAPKY003_1_1 = oChildren_FSAPKY003_1_1.Add();

                        oChild_FSAPKY003_1_1.SetProperty("U_UniteSic", item.UniteSicakligi);

                        oChild_FSAPKY003_1_1.SetProperty("U_TekneNo", item.TekneNumarasi);

                        oChild_FSAPKY003_1_1.SetProperty("U_UrtUrunAdi", item.UretilenUrunAdi);

                        oChild_FSAPKY003_1_1.SetProperty("U_AlSutMik", item.AlinanSutMiktari);

                        oChild_FSAPKY003_1_1.SetProperty("U_TekSutSic", item.TekneSutuSicakligi);

                        oChild_FSAPKY003_1_1.SetProperty("U_MayaSaat", item.MayalamaSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_BaskBasSaat", item.BaskilamaBasSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_BaskSonSaat", item.BaskilamaSonSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_KesmeSaat", item.KesmeSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_KesmePH", item.KesmePH);

                        oChild_FSAPKY003_1_1.SetProperty("U_SalIlvSaat", item.SalamuraIlaveSaati);

                        oChild_FSAPKY003_1_1.SetProperty("U_PeyTopSaat", item.PeynirTopSaat);

                        oChild_FSAPKY003_1_1.SetProperty("U_PeyTopPH", item.PeynirTopPH);

                        oChild_FSAPKY003_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY003_1_2 = oGeneralData.Child("AIF_FSAPKY003_1_2");

                    if (oChildren_FSAPKY003_1_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY003_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY003_1_2.Remove(0);
                    }

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_2s)
                    {
                        oChild_FSAPKY003_1_2 = oChildren_FSAPKY003_1_2.Add();

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalPH", item.TekneSalamuraPH);

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalBol", item.TekneSalamuraBolme);

                        oChild_FSAPKY003_1_2.SetProperty("U_TekneSalSic", item.TekneSalamuraSicakligi);
                    }

                    oChildren_FSAPKY003_1_3 = oGeneralData.Child("AIF_FSAPKY003_1_3");

                    if (oChildren_FSAPKY003_1_3.Count > 0)
                    {
                        int drc = oChildren_FSAPKY003_1_3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY003_1_3.Remove(0);
                    }

                    foreach (var item in fSAPKY003_1.fSAPKY003_1_3s)
                    {
                        oChild_FSAPKY003_1_3 = oChildren_FSAPKY003_1_3.Add();

                        oChild_FSAPKY003_1_3.SetProperty("U_NumuneNo", item.NumuneNumarasi);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutPH", item.BeyazPeynirSutPH);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutSH", item.BeyazPeynirSutSH);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutKM", item.BeyazPeynirSutKM);

                        oChild_FSAPKY003_1_3.SetProperty("U_BeyPeySutYag", item.BeyazPeynirSutYagi);

                        oChild_FSAPKY003_1_3.SetProperty("U_OprtAdi", item.OperatorAdi);
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