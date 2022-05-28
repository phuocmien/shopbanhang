using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for mienWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class mienWebService : System.Web.Services.WebService
{
    mienDataClassesDataContext db = null;
    public mienWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        db = new mienDataClassesDataContext();
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Xin chào Đoàn Phước Miền";
    }
    [WebMethod]
    public int demMonHoc()
    {
        return db.tblMonHocs.Count();
    }
    [WebMethod]
    public List<tblGiaoVien> timGV(string khoa)
    {
        List<tblGiaoVien> ds = db.tblGiaoViens.ToList();
        foreach(tblGiaoVien gv in ds)
        {
            gv.tblPhanCongs.Clear();
        }

        List<tblGiaoVien> kq = ds
            .Where(s => s.TenGV == khoa && s.MaGV=="00253")
            .ToList();
       
            
        return kq;
         
    }
    [WebMethod]
    public int dangnhap(string khoa,string mk)
    {
        List<tblGiaoVien> ds = db.tblGiaoViens.ToList();
        foreach (tblGiaoVien gv in ds)
        {
            gv.tblPhanCongs.Clear();
        }

        int kq = ds
            .Where(s => s.TenGV == khoa && s.MaGV == mk)
            .Count();

        return kq;

    }
     [WebMethod]
    public void ThemDSGV(string MaGV, string HoGV, string TenGV, string gt, string DC, string DT)
    {
       tblGiaoVien ds = new tblGiaoVien();
        ds.MaGV = MaGV;
        ds.HoGV = HoGV;
        ds.TenGV = TenGV;
        ds.GioiTinh = gt;
        ds.DiaChi = DC;
        ds.DienThoai = DT;

        db.tblGiaoViens.InsertOnSubmit(ds);
        db.SubmitChanges();
    }
[WebMethod]
    public void SuaDSGV(string MaGV, string HoGV, string TenGV, string gt, string DC, string DT)
    {
         
    var ds = (from S in db.tblGiaoViens
                      where S.MaGV == MaGV
                      select S).Single();

        ds.HoGV = HoGV;
        ds.TenGV = TenGV;
        ds.GioiTinh = gt;
        ds.DiaChi = DC;
        ds.DienThoai = DT;
                
        db.SubmitChanges();
      

    }

    [WebMethod]
    public void xoaDSGV(string MaGV)
    {

        var ds = (from S in db.tblGiaoViens
                  where S.MaGV == MaGV
                  select S).Single();

       

        db.tblGiaoViens.DeleteOnSubmit(ds);
        db.SubmitChanges();



    }
   

}
