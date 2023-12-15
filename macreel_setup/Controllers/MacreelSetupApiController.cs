using macreel_setup.Models.admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace macreel_setupwithapi.Controllers
{
    public class LoginController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        macreel_setup.App_Code.basic_function fn = new macreel_setup.App_Code.basic_function();
        [HttpPost]
        public IHttpActionResult Check_Login()
        {
            var httpRequest = HttpContext.Current.Request;
            var ad = new macreel_setup.Models.login
            {
                username = httpRequest.Form.Get("username"),
                password = httpRequest.Form.Get("password"),
            };
            macreel_setup.Models.common_response Response = fn.login(ad.username, ad.password);
            return Ok(Response);

        }
    }
    public class ManageSubAdminController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpPost]
        public IHttpActionResult Insert_SubAdmin()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var subadmin = new manage_subadmin()
                {
                    SubAdminName = httpRequest.Form.Get("SubAdminName"),
                    Password = httpRequest.Form.Get("Password"),
                    MobileNo = httpRequest.Form.Get("MobileNo"),
                    SubAdminBranch = httpRequest.Form.Get("SubAdminBranch"),
                    Gender = httpRequest.Form.Get("Gender"),
                    Designation = httpRequest.Form.Get("Designation"),
                    DOB = httpRequest.Form.Get("DOB"),
                    JoinDate = httpRequest.Form.Get("JoinDate"),
                    Salary = httpRequest.Form.Get("Salary"),
                    Qualification = httpRequest.Form.Get("Qualification"),
                    AadharNo = httpRequest.Form.Get("AadharNo"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    StateName = httpRequest.Form.Get("StateName"),
                    StateCode = httpRequest.Form.Get("StateCode"),
                    CityName = httpRequest.Form.Get("CityName"),
                    CityCode = httpRequest.Form.Get("CityCode"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    Address = httpRequest.Form.Get("Address"),
                    File_Name = httpRequest.Form.Get("File_Name"),
                    File_Path = httpRequest.Form.Get("File_Path")
                };
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    subadmin.File_Path = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }
                // Add other form fields as needed
                SqlCommand cmd = new SqlCommand("sp_SubAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_subadmin");
                cmd.Parameters.AddWithValue("@SubAdminName", subadmin.SubAdminName);
                cmd.Parameters.AddWithValue("@Password", subadmin.Password);
                cmd.Parameters.AddWithValue("@MobileNo", subadmin.MobileNo);
                cmd.Parameters.AddWithValue("@SubAdminBranch", subadmin.SubAdminBranch);
                cmd.Parameters.AddWithValue("@Gender", subadmin.Gender);
                cmd.Parameters.AddWithValue("@Designation", subadmin.Designation);
                cmd.Parameters.AddWithValue("@DOB", subadmin.DOB);
                cmd.Parameters.AddWithValue("@JoinDate", subadmin.JoinDate);
                cmd.Parameters.AddWithValue("@Salary", subadmin.Salary);
                cmd.Parameters.AddWithValue("@Qualification", subadmin.Qualification);
                cmd.Parameters.AddWithValue("@AadharNo", subadmin.AadharNo);
                cmd.Parameters.AddWithValue("@PanNo", subadmin.PanNo);
                cmd.Parameters.AddWithValue("@StateName", subadmin.StateName);
                cmd.Parameters.AddWithValue("@StateCode", subadmin.StateCode);
                cmd.Parameters.AddWithValue("@CityName", subadmin.CityName);
                cmd.Parameters.AddWithValue("@CityCode", subadmin.CityCode);
                cmd.Parameters.AddWithValue("@Pincode", subadmin.Pincode);
                cmd.Parameters.AddWithValue("@Address", subadmin.Address);
                cmd.Parameters.AddWithValue("@File_Name", subadmin.File_Name);
                cmd.Parameters.AddWithValue("@File_Path", subadmin.File_Path);
                cmd.Parameters.AddWithValue("@Id", subadmin.id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult Get_SubAdmin()
        {
            List<macreel_setup.Models.admin.manage_subadmin> subadmins = new List<macreel_setup.Models.admin.manage_subadmin>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SubAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getsubadminlist");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_subadmin sub = new manage_subadmin();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    sub = new manage_subadmin();
                    sub.id = sdr["Id"].ToString();
                    sub.SubAdminName = sdr["SubAdminName"].ToString();
                    sub.Password = sdr["Password"].ToString();
                    sub.MobileNo = sdr["MobileNo"].ToString();
                    sub.SubAdminBranch = sdr["SubAdminBranch"].ToString();
                    sub.Gender = sdr["Gender"].ToString();
                    sub.Designation = sdr["Designation"].ToString();
                    sub.DOB = sdr["DOB"].ToString();
                    sub.JoinDate = sdr["JoinDate"].ToString();
                    sub.Salary = sdr["Salary"].ToString();
                    sub.Qualification = sdr["Qualification"].ToString();
                    sub.AadharNo = sdr["AadharNo"].ToString();
                    sub.PanNo = sdr["PanNo"].ToString();
                    sub.StateName = sdr["StateName"].ToString();
                    sub.StateCode = sdr["StateCode"].ToString();
                    sub.CityName = sdr["CityName"].ToString();
                    sub.CityCode = sdr["CityCode"].ToString();
                    sub.Pincode = sdr["Pincode"].ToString();
                    sub.Address = sdr["Address"].ToString();
                    sub.File_Name = sdr["File_Name"].ToString();
                    sub.File_Path = sdr["File_Path"].ToString();
                    subadmins.Add(sub);
                }

            }
            con.Close();
            return Ok(subadmins);
        }
        [HttpGet]
        public IHttpActionResult GetSubAdminById(string id)
        {
            macreel_setup.Models.admin.manage_subadmin subadmins = new macreel_setup.Models.admin.manage_subadmin();
            subadmins = db.getsubadminById(id);
            return Ok(subadmins);
        }
        [HttpDelete]
        public IHttpActionResult DeleteSubadmin(string id)
        {
            db.delete_subadmin(id);
            return Ok("success");
        }
        [HttpPut]
        public IHttpActionResult UpdateSubAdmin(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
             
                var subadmin = new manage_subadmin()
                {
                    SubAdminName = httpRequest.Form.Get("SubAdminName"),
                    Password = httpRequest.Form.Get("Password"),
                    MobileNo = httpRequest.Form.Get("MobileNo"),
                    SubAdminBranch = httpRequest.Form.Get("SubAdminBranch"),
                    Gender = httpRequest.Form.Get("Gender"),
                    Designation = httpRequest.Form.Get("Designation"),
                    DOB = httpRequest.Form.Get("DOB"),
                    JoinDate = httpRequest.Form.Get("JoinDate"),
                    Salary = httpRequest.Form.Get("Salary"),
                    Qualification = httpRequest.Form.Get("Qualification"),
                    AadharNo = httpRequest.Form.Get("AadharNo"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    StateName = httpRequest.Form.Get("StateName"),
                    StateCode = httpRequest.Form.Get("StateCode"),
                    CityName = httpRequest.Form.Get("CityName"),
                    CityCode = httpRequest.Form.Get("CityCode"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    Address = httpRequest.Form.Get("Address"),
                    File_Name = httpRequest.Form.Get("File_Name"),
                    File_Path = httpRequest.Form.Get("File_Path")
                };
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    subadmin.File_Path = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }
                // Add other form fields as needed
                SqlCommand cmd = new SqlCommand("sp_SubAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updatesubadmin");
                cmd.Parameters.AddWithValue("@SubAdminName", subadmin.SubAdminName);
                cmd.Parameters.AddWithValue("@Password", subadmin.Password);
                cmd.Parameters.AddWithValue("@MobileNo", subadmin.MobileNo);
                cmd.Parameters.AddWithValue("@SubAdminBranch", subadmin.SubAdminBranch);
                cmd.Parameters.AddWithValue("@Gender", subadmin.Gender);
                cmd.Parameters.AddWithValue("@Designation", subadmin.Designation);
                cmd.Parameters.AddWithValue("@DOB", subadmin.DOB);
                cmd.Parameters.AddWithValue("@JoinDate", subadmin.JoinDate);
                cmd.Parameters.AddWithValue("@Salary", subadmin.Salary);
                cmd.Parameters.AddWithValue("@Qualification", subadmin.Qualification);
                cmd.Parameters.AddWithValue("@AadharNo", subadmin.AadharNo);
                cmd.Parameters.AddWithValue("@PanNo", subadmin.PanNo);
                cmd.Parameters.AddWithValue("@StateName", subadmin.StateName);
                cmd.Parameters.AddWithValue("@StateCode", subadmin.StateCode);
                cmd.Parameters.AddWithValue("@CityName", subadmin.CityName);
                cmd.Parameters.AddWithValue("@CityCode", subadmin.CityCode);
                cmd.Parameters.AddWithValue("@Pincode", subadmin.Pincode);
                cmd.Parameters.AddWithValue("@Address", subadmin.Address);
                cmd.Parameters.AddWithValue("@File_Name", subadmin.File_Name);
                cmd.Parameters.AddWithValue("@File_Path", subadmin.File_Path);
                cmd.Parameters.AddWithValue("@Id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
      
    }
    public class EmployeeManagementController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpGet]
        public IHttpActionResult Get_Employee()
        {
            List<macreel_setup.Models.admin.employee_manage> employee_manage = new List<macreel_setup.Models.admin.employee_manage>();

            string sql = "select * from employee_management order by id";

            DataTable dtr = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dtr.Rows.Count > 0)
            {
                foreach (DataRow dr in dtr.Rows)
                {
                    employee_manage.Add(new macreel_setup.Models.admin.employee_manage()
                    {
                        employee_branch = dr["employee_branch"].ToString(),
                        employee_type = dr["employee_type"].ToString(),
                        employee_designation = dr["employee_designation"].ToString(),
                        employee_id = dr["employee_id"].ToString(),
                        login_userid = dr["login_userid"].ToString(),
                        password = dr["password"].ToString(),
                        employee_first_name = dr["employee_first_name"].ToString(),
                        employee_last_name = dr["employee_last_name"].ToString(),
                        mobile_no = dr["mobile_no"].ToString(),
                        phone_no = dr["phone_no"].ToString(),
                        gender = dr["gender"].ToString(),
                        alternate_number = dr["alternate_number"].ToString(),
                        dob = dr["dob"].ToString(),
                        join_date = dr["join_date"].ToString(),
                        salary_ctc = dr["salary_ctc"].ToString(),
                        qualification = dr["qualification"].ToString(),
                        aadhar_no = dr["aadhar_no"].ToString(),
                        pan_no = dr["pan_no"].ToString(),
                        profile_picture = dr["profile_picture"].ToString(),
                        country = dr["country"].ToString(),
                        state = dr["state"].ToString(),
                        city = dr["city"].ToString(),
                        area_code = dr["area_code"].ToString(),
                        address = dr["address"].ToString(),
                        employee_department = dr["employee_department"].ToString(),
                        reporting_manager = dr["reporting_manager"].ToString(),
                        reporting_tl = dr["reporting_tl"].ToString(),
                        exit_date = dr["exit_date"].ToString(),
                        current_status = dr["current_status"].ToString(),
                        id = dr["id"].ToString(),

                    });
                }
            }
            return Ok(employee_manage);
        }
        //[HttpGet]
        //public IHttpActionResult GetEmployeeById(string id)
        //{
        //    List<macreel_setup.Models.admin.employee_manage> employee_manage = new List<macreel_setup.Models.admin.employee_manage>();

        //    employee_manage = macreel_setup.Admin.employee_management(id);

        //    return Ok(packing_model);
        //}
    }
    public class ManageSequenceNoController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpGet]
        public IHttpActionResult Get_SequenceNo()
        {
            List<manage_sequence> sequences = new List<manage_sequence>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SequenceNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllsequenceno");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_sequence seq = new manage_sequence();
            while (sdr.Read())
            {
                seq = new manage_sequence();
                seq.id = sdr["Id"].ToString();
                seq.Branch = sdr["Branch"].ToString();
                seq.QuotationPrefixNo = sdr["QuotationPrefixNo"].ToString();
                seq.QuotationNo = sdr["QuotationNo"].ToString();
                seq.SaleBookPrefixNo = sdr["SaleBookPrefixNo"].ToString();
                seq.SaleBookNo = sdr["SaleBookNo"].ToString();
                seq.PIPrefixNo = sdr["PIPrefixNo"].ToString();
                seq.PINo = sdr["PINo"].ToString();
                seq.POPrefixNo = sdr["POPrefixNo"].ToString();
                seq.PONo = sdr["PONo"].ToString();
                seq.InvoicePrefixNo = sdr["InvoicePrefixNo"].ToString();
                seq.InvoiceNo = sdr["InvoiceNo"].ToString();
                sequences.Add(seq);
            }
            con.Close();
            return Ok(sequences);
        }
        [HttpGet]
        //Get : api/ManageSequenceNo/2
        public IHttpActionResult GetSequenceById(string id)
        {
            manage_sequence sequences = new manage_sequence();
            sequences = db.getsequenceById(id);
            return Ok(sequences);
        }
        [HttpPost]
        //Post : api/ManageSequenceNo
        public IHttpActionResult Insert_Sequence()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                // Access form data from the request
                //courses.id = Convert.ToInt32(httpRequest.Form["id"]);
                //courses.course_name = httpRequest.Form["course_name"];
                var sequence = new manage_sequence()
                {
                    Branch = httpRequest.Form.Get("Branch"),
                    QuotationPrefixNo = httpRequest.Form.Get("QuotationPrefixNo"),
                    QuotationNo = httpRequest.Form.Get("QuotationNo"),
                    SaleBookPrefixNo = httpRequest.Form.Get("SaleBookPrefixNo"),
                    SaleBookNo = httpRequest.Form.Get("SaleBookNo"),
                    PIPrefixNo = httpRequest.Form.Get("PIPrefixNo"),
                    PINo = httpRequest.Form.Get("PINo"),
                    POPrefixNo = httpRequest.Form.Get("POPrefixNo"),
                    PONo = httpRequest.Form.Get("PONo"),
                    InvoicePrefixNo = httpRequest.Form.Get("InvoicePrefixNo"),
                    InvoiceNo = httpRequest.Form.Get("InvoiceNo")
                };


                SqlCommand cmd = new SqlCommand("sp_SequenceNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_sequenceNo");
                cmd.Parameters.AddWithValue("@Branch", sequence.Branch);
                cmd.Parameters.AddWithValue("@QuotationPrefixNo", sequence.QuotationPrefixNo);
                cmd.Parameters.AddWithValue("@QuotationNo", sequence.QuotationNo);
                cmd.Parameters.AddWithValue("@SaleBookPrefixNo", sequence.SaleBookPrefixNo);
                cmd.Parameters.AddWithValue("@SaleBookNo", sequence.SaleBookNo);
                cmd.Parameters.AddWithValue("@PIPrefixNo", sequence.PIPrefixNo);
                cmd.Parameters.AddWithValue("@PINo", sequence.PINo);
                cmd.Parameters.AddWithValue("@POPrefixNo", sequence.POPrefixNo);
                cmd.Parameters.AddWithValue("@PONo", sequence.PONo);
                cmd.Parameters.AddWithValue("@InvoicePrefixNo", sequence.InvoicePrefixNo);
                cmd.Parameters.AddWithValue("@InvoiceNo", sequence.InvoiceNo);
                cmd.Parameters.AddWithValue("@Id", sequence.id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpDelete]
        //Delete : api/ManageSequenceNo/1
        public IHttpActionResult DeleteSequence(string id)
        {
            db.delete_sequence(id);
            return Ok("Success");
        }
        [HttpPut]
        //Update : api/ManageSequenceNo/1
        public IHttpActionResult UpdateSequence(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                // Access form data from the request
                //courses.id = Convert.ToInt32(httpRequest.Form["id"]);
                //courses.course_name = httpRequest.Form["course_name"];
                var sequence = new manage_sequence()
                {
                    Branch = httpRequest.Form.Get("Branch"),
                    QuotationPrefixNo = httpRequest.Form.Get("QuotationPrefixNo"),
                    QuotationNo = httpRequest.Form.Get("QuotationNo"),
                    SaleBookPrefixNo = httpRequest.Form.Get("SaleBookPrefixNo"),
                    SaleBookNo = httpRequest.Form.Get("SaleBookNo"),
                    PIPrefixNo = httpRequest.Form.Get("PIPrefixNo"),
                    PINo = httpRequest.Form.Get("PINo"),
                    POPrefixNo = httpRequest.Form.Get("POPrefixNo"),
                    PONo = httpRequest.Form.Get("PONo"),
                    InvoicePrefixNo = httpRequest.Form.Get("InvoicePrefixNo"),
                    InvoiceNo = httpRequest.Form.Get("InvoiceNo")
                };


                SqlCommand cmd = new SqlCommand("sp_SequenceNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updatesequence");
                cmd.Parameters.AddWithValue("@Branch", sequence.Branch);
                cmd.Parameters.AddWithValue("@QuotationPrefixNo", sequence.QuotationPrefixNo);
                cmd.Parameters.AddWithValue("@QuotationNo", sequence.QuotationNo);
                cmd.Parameters.AddWithValue("@SaleBookPrefixNo", sequence.SaleBookPrefixNo);
                cmd.Parameters.AddWithValue("@SaleBookNo", sequence.SaleBookNo);
                cmd.Parameters.AddWithValue("@PIPrefixNo", sequence.PIPrefixNo);
                cmd.Parameters.AddWithValue("@PINo", sequence.PINo);
                cmd.Parameters.AddWithValue("@POPrefixNo", sequence.POPrefixNo);
                cmd.Parameters.AddWithValue("@PONo", sequence.PONo);
                cmd.Parameters.AddWithValue("@InvoicePrefixNo", sequence.InvoicePrefixNo);
                cmd.Parameters.AddWithValue("@InvoiceNo", sequence.InvoiceNo);
                cmd.Parameters.AddWithValue("@Id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
    }
    public class ManageProductMasterController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();

        [System.Web.Http.HttpGet]
        //Get : api/ManageProductMaster
        public IHttpActionResult Get_Product()
        {
            List<manage_product> products = new List<manage_product>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ProductMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllproduct");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_product pro = new manage_product();
            while (sdr.Read())
            {
                pro = new manage_product();
                pro.id = sdr["Id"].ToString();
                pro.ProductName = sdr["ProductName"].ToString();
                pro.OEM = sdr["OEM"].ToString();
                pro.licenceNo = sdr["licenceNo"].ToString();
                pro.PartNo = sdr["PartNo"].ToString();
                pro.Price = sdr["Price"].ToString();
                pro.HSNNo = sdr["HSNNo"].ToString();
                pro.AMCPrice = sdr["AMCPrice"].ToString();
                pro.Description = sdr["Description"].ToString();
                pro.File_Path = sdr["File_Path"].ToString();
                pro.File_Name = sdr["File_Name"].ToString();
                products.Add(pro);
            }
            con.Close();
            return Ok(products);
        }
        //[System.Web.Http.HttpGet]
        ////Get : api/ManageProductMaster/2
        public IHttpActionResult GetProductById(string id)
        {
            manage_product products = new manage_product();
            products = db.getproductbyId(id);
            return Ok(products);
        }
        [System.Web.Http.HttpPost]
        //Insert : api/ManageProductMaster
        public IHttpActionResult Insert_Product()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var products = new manage_product()
                {
                    ProductName = httpRequest.Form.Get("ProductName"),
                    OEM = httpRequest.Form.Get("OEM"),
                    licenceNo = httpRequest.Form.Get("licenceNo"),
                    PartNo = httpRequest.Form.Get("PartNo"),
                    Price = httpRequest.Form.Get("Price"),
                    HSNNo = httpRequest.Form.Get("HSNNo"),
                    AMCPrice = httpRequest.Form.Get("AMCPrice"),
                    Description = httpRequest.Form.Get("Description"),
                    File_Path = httpRequest.Form.Get("File_Path"),
                    File_Name = httpRequest.Form.Get("File_Name"),
                };
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    products.File_Path = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }

                SqlCommand cmd = new SqlCommand("sp_ProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_product");
                cmd.Parameters.AddWithValue("@Id", products.id);
                cmd.Parameters.AddWithValue("@ProductName", products.ProductName);
                cmd.Parameters.AddWithValue("@OEM", products.OEM);
                cmd.Parameters.AddWithValue("@licenceNo", products.licenceNo);
                cmd.Parameters.AddWithValue("@PartNo", products.PartNo);
                cmd.Parameters.AddWithValue("@Price", products.Price);
                cmd.Parameters.AddWithValue("@HSNNo", products.HSNNo);
                cmd.Parameters.AddWithValue("@AMCPrice", products.AMCPrice);
                cmd.Parameters.AddWithValue("@Description", products.Description);
                cmd.Parameters.AddWithValue("@File_Path", products.File_Path);
                cmd.Parameters.AddWithValue("@File_Name", products.File_Name);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpPut]
        //Update : api/ManageProductMaster/3
        public IHttpActionResult UpdateProduct(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var products = new manage_product()
                {
                    ProductName = httpRequest.Form.Get("ProductName"),
                    OEM = httpRequest.Form.Get("OEM"),
                    licenceNo = httpRequest.Form.Get("licenceNo"),
                    PartNo = httpRequest.Form.Get("PartNo"),
                    Price = httpRequest.Form.Get("Price"),
                    HSNNo = httpRequest.Form.Get("HSNNo"),
                    AMCPrice = httpRequest.Form.Get("AMCPrice"),
                    Description = httpRequest.Form.Get("Description"),
                    File_Path = httpRequest.Form.Get("File_Path"),
                    File_Name = httpRequest.Form.Get("File_Name"),
                };
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    products.File_Path = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }

                SqlCommand cmd = new SqlCommand("sp_ProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateproductbyId");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ProductName", products.ProductName);
                cmd.Parameters.AddWithValue("@OEM", products.OEM);
                cmd.Parameters.AddWithValue("@licenceNo", products.licenceNo);
                cmd.Parameters.AddWithValue("@PartNo", products.PartNo);
                cmd.Parameters.AddWithValue("@Price", products.Price);
                cmd.Parameters.AddWithValue("@HSNNo", products.HSNNo);
                cmd.Parameters.AddWithValue("@AMCPrice", products.AMCPrice);
                cmd.Parameters.AddWithValue("@Description", products.Description);
                cmd.Parameters.AddWithValue("@File_Path", products.File_Path);
                cmd.Parameters.AddWithValue("@File_Name", products.File_Name);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpDelete]
        //Update : api/ManageProductMaster/3
        public IHttpActionResult DeleteProduct(string id)
        {
            db.delete_product(id);
            return Ok("Success");
        }
    }
    public class ManageVendorController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpGet]
        //Get : api/ManageVendor
        public IHttpActionResult Get_Vendor()
        {
            List<manage_vendor> vendors = new List<manage_vendor>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_VendorMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllVendor");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_vendor ven = new manage_vendor();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ven = new manage_vendor();
                    ven.id = sdr["Id"].ToString();
                    ven.VendoreName = sdr["VendoreName"].ToString();
                    ven.ContactPerson = sdr["ContactPerson"].ToString();
                    ven.ContactNO = sdr["ContactNO"].ToString();
                    ven.EmailId = sdr["EmailId"].ToString();
                    ven.StateName = sdr["StateName"].ToString();
                    ven.StateCode = sdr["StateCode"].ToString();
                    ven.CityName = sdr["CityName"].ToString();
                    ven.CityCode = sdr["CityCode"].ToString();
                    ven.Pincode = sdr["Pincode"].ToString();
                    ven.GSTNo = sdr["GSTNo"].ToString();
                    ven.PanNo = sdr["PanNo"].ToString();
                    ven.Address = sdr["Address"].ToString();
                    vendors.Add(ven);
                }
            }
            sdr.Close();
            con.Close();
            return Ok(vendors);
        }
        //[HttpGet]
        ////Get : api/ManageVendor/2
        public IHttpActionResult GetVendorById(string id)
        {
            manage_vendor vendors = new manage_vendor();
            vendors = db.getvendorById(id);
            return Ok(vendors);
        }
        [HttpPost]
        //Insert : api/ManageVendor
        public IHttpActionResult insert_Vendor()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var vendor = new manage_vendor()
                {
                    VendoreName = httpRequest.Form.Get("VendoreName"),
                    ContactPerson = httpRequest.Form.Get("ContactPerson"),
                    ContactNO = httpRequest.Form.Get("ContactNO"),
                    EmailId = httpRequest.Form.Get("EmailId"),
                    StateName = httpRequest.Form.Get("StateName"),
                    StateCode = httpRequest.Form.Get("StateCode"),
                    CityName = httpRequest.Form.Get("CityName"),
                    CityCode = httpRequest.Form.Get("CityCode"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    GSTNo = httpRequest.Form.Get("GSTNo"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    Address = httpRequest.Form.Get("Address")
                };

                SqlCommand cmd = new SqlCommand("sp_VendorMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_Vendor");
                cmd.Parameters.AddWithValue("@Id", vendor.id);
                cmd.Parameters.AddWithValue("@VendoreName", vendor.VendoreName);
                cmd.Parameters.AddWithValue("@ContactPerson", vendor.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNO", vendor.ContactNO);
                cmd.Parameters.AddWithValue("@EmailId", vendor.EmailId);
                cmd.Parameters.AddWithValue("@StateName", vendor.StateName);
                cmd.Parameters.AddWithValue("@StateCode", vendor.StateCode);
                cmd.Parameters.AddWithValue("@CityName", vendor.CityName);
                cmd.Parameters.AddWithValue("@CityCode", vendor.CityCode);
                cmd.Parameters.AddWithValue("@Pincode", vendor.Pincode);
                cmd.Parameters.AddWithValue("@GSTNo", vendor.GSTNo);
                cmd.Parameters.AddWithValue("@PanNo", vendor.PanNo);
                cmd.Parameters.AddWithValue("@Address", vendor.Address);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpPut]
        //Insert : api/ManageVendor
        public IHttpActionResult UpdateVendor(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var vendor = new manage_vendor()
                {
                    VendoreName = httpRequest.Form.Get("VendoreName"),
                    ContactPerson = httpRequest.Form.Get("ContactPerson"),
                    ContactNO = httpRequest.Form.Get("ContactNO"),
                    EmailId = httpRequest.Form.Get("EmailId"),
                    StateName = httpRequest.Form.Get("StateName"),
                    StateCode = httpRequest.Form.Get("StateCode"),
                    CityName = httpRequest.Form.Get("CityName"),
                    CityCode = httpRequest.Form.Get("CityCode"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    GSTNo = httpRequest.Form.Get("GSTNo"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    Address = httpRequest.Form.Get("Address")
                };

                SqlCommand cmd = new SqlCommand("sp_VendorMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateVendorbyId");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@VendoreName", vendor.VendoreName);
                cmd.Parameters.AddWithValue("@ContactPerson", vendor.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNO", vendor.ContactNO);
                cmd.Parameters.AddWithValue("@EmailId", vendor.EmailId);
                cmd.Parameters.AddWithValue("@StateName", vendor.StateName);
                cmd.Parameters.AddWithValue("@StateCode", vendor.StateCode);
                cmd.Parameters.AddWithValue("@CityName", vendor.CityName);
                cmd.Parameters.AddWithValue("@CityCode", vendor.CityCode);
                cmd.Parameters.AddWithValue("@Pincode", vendor.Pincode);
                cmd.Parameters.AddWithValue("@GSTNo", vendor.GSTNo);
                cmd.Parameters.AddWithValue("@PanNo", vendor.PanNo);
                cmd.Parameters.AddWithValue("@Address", vendor.Address);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpDelete]
        //Insert : api/ManageVendor
        public IHttpActionResult DeleteVendor(string id)
        {
            db.delete_vendor(id);
            return Ok("Success");
        }
    }
    public class ManageSaleBookController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [System.Web.Http.HttpGet]
        //Get : api/ManageSaleBook
        public IHttpActionResult Get_SalesBook()
        {
            List<manage_salebook> salebooks = new List<manage_salebook>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_SaleBook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllSaleBook");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_salebook book = new manage_salebook();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    book = new manage_salebook();
                    book.id = sdr["Id"].ToString();
                    book.OrderNo = sdr["OrderNo"].ToString();
                    book.QuotationReference = sdr["QuotationReference"].ToString();
                    book.LeadReference = sdr["LeadReference"].ToString();
                    book.Employee = sdr["Employee"].ToString();
                    book.Branch = sdr["Branch"].ToString();
                    book.SelectBillTO = sdr["SelectBillTO"].ToString();
                    book.Date = sdr["Date"].ToString();
                    book.SelectShipTO = sdr["SelectShipTO"].ToString();
                    book.Add_PORNo = sdr["Add_PORNo"].ToString();
                    book.LANID = sdr["LANID"].ToString();
                    book.PromoCode = sdr["PromoCode"].ToString();
                    book.DomainID = sdr["DomainID"].ToString();
                    book.RenewalStatus = sdr["RenewalStatus"].ToString();
                    book.RenewalPeriod = sdr["RenewalPeriod"].ToString();
                    book.RenewalEndDate = sdr["RenewalEndDate"].ToString();
                    book.RenewalCount = sdr["RenewalCount"].ToString();
                    book.BillingStatus = sdr["BillingStatus"].ToString();
                    book.BillingPeriod = sdr["BillingPeriod"].ToString();
                    book.BillingEndDate = sdr["BillingEndDate"].ToString();
                    book.BillingCount = sdr["BillingCount"].ToString();
                    book.InState_OutState = sdr["InState_OutState"].ToString();
                    book.Tax = sdr["Tax"].ToString();
                    book.TAXAmount = sdr["TAXAmount"].ToString();
                    book.PriceBeforeTAX = sdr["PriceBeforeTAX"].ToString();
                    book.PriceAfterTAX = sdr["PriceAfterTAX"].ToString();
                    book.SaleGeneratedDate = sdr["SaleGeneratedDate"].ToString();
                    book.SaleGeneratedDate = sdr["SaleGeneratedDate"].ToString();
                    salebooks.Add(book);

                }
            }
            sdr.Close();
            con.Close();
            return Ok(salebooks);
        }
        //[System.Web.Http.HttpGet]
        ////Get : api/ManageSaleBook/1
        public IHttpActionResult GetSalesBookById(string id)
        {
            manage_salebook salebooks = new manage_salebook();
            salebooks = db.getsalebookbyId(id);
            return Ok(salebooks);
        }
        [System.Web.Http.HttpPost]
        //Get : api/ManageSaleBook
        public IHttpActionResult Insert_SalesBook()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var salebook = new manage_salebook()
                {
                    OrderNo = httpRequest.Form.Get("OrderNo"),
                    QuotationReference = httpRequest.Form.Get("QuotationReference"),
                    LeadReference = httpRequest.Form.Get("LeadReference"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    SelectBillTO = httpRequest.Form.Get("SelectBillTO"),
                    Date = httpRequest.Form.Get("Date"),
                    SelectShipTO = httpRequest.Form.Get("SelectShipTO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LANID = httpRequest.Form.Get("LANID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    RenewalStatus = httpRequest.Form.Get("RenewalStatus"),
                    RenewalPeriod = httpRequest.Form.Get("RenewalPeriod"),
                    RenewalEndDate = httpRequest.Form.Get("RenewalEndDate"),
                    RenewalCount = httpRequest.Form.Get("RenewalCount"),
                    BillingStatus = httpRequest.Form.Get("BillingStatus"),
                    BillingPeriod = httpRequest.Form.Get("BillingPeriod"),
                    BillingEndDate = httpRequest.Form.Get("BillingEndDate"),
                    BillingCount = httpRequest.Form.Get("BillingCount"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAXAmount = httpRequest.Form.Get("TAXAmount"),
                    PriceBeforeTAX = httpRequest.Form.Get("PriceBeforeTAX"),
                    PriceAfterTAX = httpRequest.Form.Get("PriceAfterTAX"),
                    SaleGeneratedDate = httpRequest.Form.Get("SaleGeneratedDate")
                };
                SqlCommand cmd = new SqlCommand("sp_SaleBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_SaleBook");
                cmd.Parameters.AddWithValue("@Id", salebook.id);
                cmd.Parameters.AddWithValue("@OrderNo", salebook.OrderNo);
                cmd.Parameters.AddWithValue("@QuotationReference", salebook.QuotationReference);
                cmd.Parameters.AddWithValue("@LeadReference", salebook.LeadReference);
                cmd.Parameters.AddWithValue("@Employee", salebook.Employee);
                cmd.Parameters.AddWithValue("@Branch", salebook.Branch);
                cmd.Parameters.AddWithValue("@SelectBillTO", salebook.SelectBillTO);
                cmd.Parameters.AddWithValue("@Date", salebook.Date);
                cmd.Parameters.AddWithValue("@SelectShipTO", salebook.SelectShipTO);
                cmd.Parameters.AddWithValue("@Add_PORNo", salebook.Add_PORNo);
                cmd.Parameters.AddWithValue("@LANID", salebook.LANID);
                cmd.Parameters.AddWithValue("@PromoCode", salebook.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", salebook.DomainID);
                cmd.Parameters.AddWithValue("@RenewalStatus", salebook.RenewalStatus);
                cmd.Parameters.AddWithValue("@RenewalPeriod", salebook.RenewalPeriod);
                cmd.Parameters.AddWithValue("@RenewalEndDate", salebook.RenewalEndDate);
                cmd.Parameters.AddWithValue("@RenewalCount", salebook.RenewalCount);
                cmd.Parameters.AddWithValue("@BillingStatus", salebook.BillingStatus);
                cmd.Parameters.AddWithValue("@BillingPeriod", salebook.BillingPeriod);
                cmd.Parameters.AddWithValue("@BillingEndDate", salebook.BillingEndDate);
                cmd.Parameters.AddWithValue("@BillingCount", salebook.BillingCount);
                cmd.Parameters.AddWithValue("@InState_OutState", salebook.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", salebook.Tax);
                cmd.Parameters.AddWithValue("@TAXAmount", salebook.TAXAmount);
                cmd.Parameters.AddWithValue("@PriceBeforeTAX", salebook.PriceBeforeTAX);
                cmd.Parameters.AddWithValue("@PriceAfterTAX", salebook.PriceAfterTAX);
                cmd.Parameters.AddWithValue("@SaleGeneratedDate", salebook.SaleGeneratedDate);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpPut]
        //Update : api/ManageSaleBook/1
        public IHttpActionResult UpdateSalesBook(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var salebook = new manage_salebook()
                {
                    OrderNo = httpRequest.Form.Get("OrderNo"),
                    QuotationReference = httpRequest.Form.Get("QuotationReference"),
                    LeadReference = httpRequest.Form.Get("LeadReference"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    SelectBillTO = httpRequest.Form.Get("SelectBillTO"),
                    Date = httpRequest.Form.Get("Date"),
                    SelectShipTO = httpRequest.Form.Get("SelectShipTO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LANID = httpRequest.Form.Get("LANID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    RenewalStatus = httpRequest.Form.Get("RenewalStatus"),
                    RenewalPeriod = httpRequest.Form.Get("RenewalPeriod"),
                    RenewalEndDate = httpRequest.Form.Get("RenewalEndDate"),
                    RenewalCount = httpRequest.Form.Get("RenewalCount"),
                    BillingStatus = httpRequest.Form.Get("BillingStatus"),
                    BillingPeriod = httpRequest.Form.Get("BillingPeriod"),
                    BillingEndDate = httpRequest.Form.Get("BillingEndDate"),
                    BillingCount = httpRequest.Form.Get("BillingCount"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAXAmount = httpRequest.Form.Get("TAXAmount"),
                    PriceBeforeTAX = httpRequest.Form.Get("PriceBeforeTAX"),
                    PriceAfterTAX = httpRequest.Form.Get("PriceAfterTAX"),
                    SaleGeneratedDate = httpRequest.Form.Get("SaleGeneratedDate")
                };
                SqlCommand cmd = new SqlCommand("sp_SaleBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateSaleBookbyId");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@OrderNo", salebook.OrderNo);
                cmd.Parameters.AddWithValue("@QuotationReference", salebook.QuotationReference);
                cmd.Parameters.AddWithValue("@LeadReference", salebook.LeadReference);
                cmd.Parameters.AddWithValue("@Employee", salebook.Employee);
                cmd.Parameters.AddWithValue("@Branch", salebook.Branch);
                cmd.Parameters.AddWithValue("@SelectBillTO", salebook.SelectBillTO);
                cmd.Parameters.AddWithValue("@Date", salebook.Date);
                cmd.Parameters.AddWithValue("@SelectShipTO", salebook.SelectShipTO);
                cmd.Parameters.AddWithValue("@Add_PORNo", salebook.Add_PORNo);
                cmd.Parameters.AddWithValue("@LANID", salebook.LANID);
                cmd.Parameters.AddWithValue("@PromoCode", salebook.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", salebook.DomainID);
                cmd.Parameters.AddWithValue("@RenewalStatus", salebook.RenewalStatus);
                cmd.Parameters.AddWithValue("@RenewalPeriod", salebook.RenewalPeriod);
                cmd.Parameters.AddWithValue("@RenewalEndDate", salebook.RenewalEndDate);
                cmd.Parameters.AddWithValue("@RenewalCount", salebook.RenewalCount);
                cmd.Parameters.AddWithValue("@BillingStatus", salebook.BillingStatus);
                cmd.Parameters.AddWithValue("@BillingPeriod", salebook.BillingPeriod);
                cmd.Parameters.AddWithValue("@BillingEndDate", salebook.BillingEndDate);
                cmd.Parameters.AddWithValue("@BillingCount", salebook.BillingCount);
                cmd.Parameters.AddWithValue("@InState_OutState", salebook.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", salebook.Tax);
                cmd.Parameters.AddWithValue("@TAXAmount", salebook.TAXAmount);
                cmd.Parameters.AddWithValue("@PriceBeforeTAX", salebook.PriceBeforeTAX);
                cmd.Parameters.AddWithValue("@PriceAfterTAX", salebook.PriceAfterTAX);
                cmd.Parameters.AddWithValue("@SaleGeneratedDate", salebook.SaleGeneratedDate);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpDelete]
        //Update : api/ManageSaleBook/1
        public IHttpActionResult DeleteSalesBook(string id)
        {
            db.delete_salebook(id);
            return Ok("Success");
        }
    }
    public class ManagePIController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [System.Web.Http.HttpGet]
        //Get : api/ManagePI
        public IHttpActionResult Get_PI()
        {
            List<manage_pi> pilist = new List<manage_pi>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PI", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllPI");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_pi pi = new manage_pi();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pi = new manage_pi();
                    pi.id = sdr["Id"].ToString();
                    pi.PINo = sdr["PINo"].ToString();
                    pi.OrderNo = sdr["OrderNo"].ToString();
                    pi.QuotationReference = sdr["QuotationReference"].ToString();
                    pi.LeadReference = sdr["LeadReference"].ToString();
                    pi.Employee = sdr["Employee"].ToString();
                    pi.Branch = sdr["Branch"].ToString();
                    pi.SelectBillTO = sdr["SelectBillTO"].ToString();
                    pi.Date = sdr["Date"].ToString();
                    pi.SelectShipTO = sdr["SelectShipTO"].ToString();
                    pi.Add_PORNo = sdr["Add_PORNo"].ToString();
                    pi.LANID = sdr["LANID"].ToString();
                    pi.PromoCode = sdr["PromoCode"].ToString();
                    pi.DomainID = sdr["DomainID"].ToString();
                    pi.RenewalStatus = sdr["RenewalStatus"].ToString();
                    pi.RenewalPeriod = sdr["RenewalPeriod"].ToString();
                    pi.RenewalEndDate = sdr["RenewalEndDate"].ToString();
                    pi.RenewalCount = sdr["RenewalCount"].ToString();
                    pi.BillingStatus = sdr["BillingStatus"].ToString();
                    pi.BillingPeriod = sdr["BillingPeriod"].ToString();
                    pi.BillingEndDate = sdr["BillingEndDate"].ToString();
                    pi.BillingCount = sdr["BillingCount"].ToString();
                    pi.InState_OutState = sdr["InState_OutState"].ToString();
                    pi.Tax = sdr["Tax"].ToString();
                    pi.TAXAmount = sdr["TAXAmount"].ToString();
                    pi.PriceBeforeTAX = sdr["PriceBeforeTAX"].ToString();
                    pi.PriceAfterTAX = sdr["PriceAfterTAX"].ToString();
                    pi.SaleGeneratedDate = sdr["SaleGeneratedDate"].ToString();
                    pi.SaleGeneratedDate = sdr["SaleGeneratedDate"].ToString();
                    pilist.Add(pi);

                }
            }
            sdr.Close();
            con.Close();
            return Ok(pilist);
        }
        //[System.Web.Http.HttpGet]
        ////Get : api/ManagePI/1
        public IHttpActionResult GetPIById(string id)
        {
            manage_pi pilist = new manage_pi();
            pilist = db.getPIbyId(id);
            return Ok(pilist);
        }
        [System.Web.Http.HttpPost]
        //Get : api/ManagePI
        public IHttpActionResult Insert_PI()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var pi = new manage_pi()
                {
                    PINo = httpRequest.Form.Get("PINo"),
                    OrderNo = httpRequest.Form.Get("OrderNo"),
                    QuotationReference = httpRequest.Form.Get("QuotationReference"),
                    LeadReference = httpRequest.Form.Get("LeadReference"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    SelectBillTO = httpRequest.Form.Get("SelectBillTO"),
                    Date = httpRequest.Form.Get("Date"),
                    SelectShipTO = httpRequest.Form.Get("SelectShipTO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LANID = httpRequest.Form.Get("LANID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    RenewalStatus = httpRequest.Form.Get("RenewalStatus"),
                    RenewalPeriod = httpRequest.Form.Get("RenewalPeriod"),
                    RenewalEndDate = httpRequest.Form.Get("RenewalEndDate"),
                    RenewalCount = httpRequest.Form.Get("RenewalCount"),
                    BillingStatus = httpRequest.Form.Get("BillingStatus"),
                    BillingPeriod = httpRequest.Form.Get("BillingPeriod"),
                    BillingEndDate = httpRequest.Form.Get("BillingEndDate"),
                    BillingCount = httpRequest.Form.Get("BillingCount"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAXAmount = httpRequest.Form.Get("TAXAmount"),
                    PriceBeforeTAX = httpRequest.Form.Get("PriceBeforeTAX"),
                    PriceAfterTAX = httpRequest.Form.Get("PriceAfterTAX"),
                    SaleGeneratedDate = httpRequest.Form.Get("SaleGeneratedDate")
                };
                SqlCommand cmd = new SqlCommand("sp_PI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_PI");
                cmd.Parameters.AddWithValue("@PINo", pi.PINo);
                cmd.Parameters.AddWithValue("@OrderNo", pi.OrderNo);
                cmd.Parameters.AddWithValue("@QuotationReference", pi.QuotationReference);
                cmd.Parameters.AddWithValue("@LeadReference", pi.LeadReference);
                cmd.Parameters.AddWithValue("@Employee", pi.Employee);
                cmd.Parameters.AddWithValue("@Branch", pi.Branch);
                cmd.Parameters.AddWithValue("@SelectBillTO", pi.SelectBillTO);
                cmd.Parameters.AddWithValue("@Date", pi.Date);
                cmd.Parameters.AddWithValue("@SelectShipTO", pi.SelectShipTO);
                cmd.Parameters.AddWithValue("@Add_PORNo", pi.Add_PORNo);
                cmd.Parameters.AddWithValue("@LANID", pi.LANID);
                cmd.Parameters.AddWithValue("@PromoCode", pi.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", pi.DomainID);
                cmd.Parameters.AddWithValue("@RenewalStatus", pi.RenewalStatus);
                cmd.Parameters.AddWithValue("@RenewalPeriod", pi.RenewalPeriod);
                cmd.Parameters.AddWithValue("@RenewalEndDate", pi.RenewalEndDate);
                cmd.Parameters.AddWithValue("@RenewalCount", pi.RenewalCount);
                cmd.Parameters.AddWithValue("@BillingStatus", pi.BillingStatus);
                cmd.Parameters.AddWithValue("@BillingPeriod", pi.BillingPeriod);
                cmd.Parameters.AddWithValue("@BillingEndDate", pi.BillingEndDate);
                cmd.Parameters.AddWithValue("@BillingCount", pi.BillingCount);
                cmd.Parameters.AddWithValue("@InState_OutState", pi.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", pi.Tax);
                cmd.Parameters.AddWithValue("@TAXAmount", pi.TAXAmount);
                cmd.Parameters.AddWithValue("@PriceBeforeTAX", pi.PriceBeforeTAX);
                cmd.Parameters.AddWithValue("@PriceAfterTAX", pi.PriceAfterTAX);
                cmd.Parameters.AddWithValue("@SaleGeneratedDate", pi.SaleGeneratedDate);
                cmd.Parameters.AddWithValue("@Id", pi.id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpPut]
        //Update : api/ManagePI/1
        public IHttpActionResult UpdatePI(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var pi = new manage_pi()
                {
                    PINo = httpRequest.Form.Get("PINo"),
                    OrderNo = httpRequest.Form.Get("OrderNo"),
                    QuotationReference = httpRequest.Form.Get("QuotationReference"),
                    LeadReference = httpRequest.Form.Get("LeadReference"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    SelectBillTO = httpRequest.Form.Get("SelectBillTO"),
                    Date = httpRequest.Form.Get("Date"),
                    SelectShipTO = httpRequest.Form.Get("SelectShipTO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LANID = httpRequest.Form.Get("LANID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    RenewalStatus = httpRequest.Form.Get("RenewalStatus"),
                    RenewalPeriod = httpRequest.Form.Get("RenewalPeriod"),
                    RenewalEndDate = httpRequest.Form.Get("RenewalEndDate"),
                    RenewalCount = httpRequest.Form.Get("RenewalCount"),
                    BillingStatus = httpRequest.Form.Get("BillingStatus"),
                    BillingPeriod = httpRequest.Form.Get("BillingPeriod"),
                    BillingEndDate = httpRequest.Form.Get("BillingEndDate"),
                    BillingCount = httpRequest.Form.Get("BillingCount"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAXAmount = httpRequest.Form.Get("TAXAmount"),
                    PriceBeforeTAX = httpRequest.Form.Get("PriceBeforeTAX"),
                    PriceAfterTAX = httpRequest.Form.Get("PriceAfterTAX"),
                    SaleGeneratedDate = httpRequest.Form.Get("SaleGeneratedDate")
                };
                SqlCommand cmd = new SqlCommand("sp_PI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updatePIbyId");
                cmd.Parameters.AddWithValue("@PINo", pi.PINo);
                cmd.Parameters.AddWithValue("@OrderNo", pi.OrderNo);
                cmd.Parameters.AddWithValue("@QuotationReference", pi.QuotationReference);
                cmd.Parameters.AddWithValue("@LeadReference", pi.LeadReference);
                cmd.Parameters.AddWithValue("@Employee", pi.Employee);
                cmd.Parameters.AddWithValue("@Branch", pi.Branch);
                cmd.Parameters.AddWithValue("@SelectBillTO", pi.SelectBillTO);
                cmd.Parameters.AddWithValue("@Date", pi.Date);
                cmd.Parameters.AddWithValue("@SelectShipTO", pi.SelectShipTO);
                cmd.Parameters.AddWithValue("@Add_PORNo", pi.Add_PORNo);
                cmd.Parameters.AddWithValue("@LANID", pi.LANID);
                cmd.Parameters.AddWithValue("@PromoCode", pi.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", pi.DomainID);
                cmd.Parameters.AddWithValue("@RenewalStatus", pi.RenewalStatus);
                cmd.Parameters.AddWithValue("@RenewalPeriod", pi.RenewalPeriod);
                cmd.Parameters.AddWithValue("@RenewalEndDate", pi.RenewalEndDate);
                cmd.Parameters.AddWithValue("@RenewalCount", pi.RenewalCount);
                cmd.Parameters.AddWithValue("@BillingStatus", pi.BillingStatus);
                cmd.Parameters.AddWithValue("@BillingPeriod", pi.BillingPeriod);
                cmd.Parameters.AddWithValue("@BillingEndDate", pi.BillingEndDate);
                cmd.Parameters.AddWithValue("@BillingCount", pi.BillingCount);
                cmd.Parameters.AddWithValue("@InState_OutState", pi.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", pi.Tax);
                cmd.Parameters.AddWithValue("@TAXAmount", pi.TAXAmount);
                cmd.Parameters.AddWithValue("@PriceBeforeTAX", pi.PriceBeforeTAX);
                cmd.Parameters.AddWithValue("@PriceAfterTAX", pi.PriceAfterTAX);
                cmd.Parameters.AddWithValue("@SaleGeneratedDate", pi.SaleGeneratedDate);
                cmd.Parameters.AddWithValue("@Id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [System.Web.Http.HttpDelete]
        //Update : api/ManagePI/1
        public IHttpActionResult DeletePI(string id)
        {
            db.delete_PI(id);
            return Ok("Success");
        }

    }
    public class ManagePOController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpGet]
        //Get : api/ManagePO
        public IHttpActionResult Get_PO()
        {
            List<manage_po> polist = new List<manage_po>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getAllPO");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_po po = new manage_po();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    po = new manage_po();
                    po.id = sdr["Id"].ToString();
                    po.PO_Number = sdr["PO_Number"].ToString();
                    po.Vendor_Id = sdr["Vendor_Id"].ToString();
                    po.Client_ID = sdr["Client_ID"].ToString();
                    po.PO_Date = sdr["PO_Date"].ToString();
                    po.Manager = sdr["Manager"].ToString();
                    po.Vendor_Email = sdr["Vendor_Email"].ToString();
                    po.LAN = sdr["LAN"].ToString();
                    po.PromoCode = sdr["PromoCode"].ToString();
                    po.SDF_Order = sdr["SDF_Order"].ToString();
                    po.MS_Account_Manager = sdr["MS_Account_Manager"].ToString();
                    po.Payment_InDays = sdr["Payment_InDays"].ToString();
                    po.Delivery_In_Days = sdr["Delivery_In_Days"].ToString();
                    po.Delivery_Mode = sdr["Delivery_Mode"].ToString();
                    po.MPN_ID = sdr["MPN_ID"].ToString();
                    po.AEP_Authorization_No = sdr["AEP_Authorization_No"].ToString();
                    po.DomainID = sdr["DomainID"].ToString();
                    po.CDC_Discount = sdr["CDC_Discount"].ToString();
                    po.CDC_Discount_Amount = sdr["CDC_Discount_Amount"].ToString();
                    po.After_Discount_Amount = sdr["After_Discount_Amount"].ToString();
                    po.IGST_Tax = sdr["IGST_Tax"].ToString();
                    po.Tax_Amount = sdr["Tax_Amount"].ToString();
                    po.After_Tax_Amount = sdr["After_Tax_Amount"].ToString();
                    po.CN = sdr["CN"].ToString();
                    po.CN_Amount = sdr["CN_Amount"].ToString();
                    po.Grand_Total_Amount = sdr["Grand_Total_Amount"].ToString();
                    po.Purchase_Amount = sdr["Purchase_Amount"].ToString();
                    po.Total_Sales_Amount = sdr["Total_Sales_Amount"].ToString();
                    po.PL_Amount = sdr["PL_Amount"].ToString();
                    polist.Add(po);

                }
            }
            sdr.Close();
            con.Close();
            return Ok(polist);
        }
        [HttpGet]
        //Get : api/ManagePO/1
        public IHttpActionResult GetPOById(string id)
        {
            manage_po polist = new manage_po();
            polist = db.getPObyId(id);
            return Ok(polist);
        }
        [HttpPost]
        //Insert : api/ManagePO
        public IHttpActionResult Insert_PO()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var po = new manage_po()
                {
                    PO_Number = httpRequest.Form.Get("PO_Number"),
                    Vendor_Id = httpRequest.Form.Get("Vendor_Id"),
                    Client_ID = httpRequest.Form.Get("Client_ID"),
                    PO_Date = httpRequest.Form.Get("PO_Date"),
                    Manager = httpRequest.Form.Get("Manager"),
                    Vendor_Email = httpRequest.Form.Get("Vendor_Email"),
                    LAN = httpRequest.Form.Get("LAN"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    SDF_Order = httpRequest.Form.Get("SDF_Order"),
                    MS_Account_Manager = httpRequest.Form.Get("MS_Account_Manager"),
                    Payment_InDays = httpRequest.Form.Get("Payment_InDays"),
                    Delivery_In_Days = httpRequest.Form.Get("Delivery_In_Days"),
                    Delivery_Mode = httpRequest.Form.Get("Delivery_Mode"),
                    MPN_ID = httpRequest.Form.Get("MPN_ID"),
                    AEP_Authorization_No = httpRequest.Form.Get("AEP_Authorization_No"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    CDC_Discount = httpRequest.Form.Get("CDC_Discount"),
                    CDC_Discount_Amount = httpRequest.Form.Get("CDC_Discount_Amount"),
                    After_Discount_Amount = httpRequest.Form.Get("After_Discount_Amount"),
                    IGST_Tax = httpRequest.Form.Get("IGST_Tax"),
                    Tax_Amount = httpRequest.Form.Get("Tax_Amount"),
                    After_Tax_Amount = httpRequest.Form.Get("After_Tax_Amount"),
                    CN = httpRequest.Form.Get("CN"),
                    CN_Amount = httpRequest.Form.Get("CN_Amount"),
                    Grand_Total_Amount = httpRequest.Form.Get("Grand_Total_Amount"),
                    Purchase_Amount = httpRequest.Form.Get("Purchase_Amount"),
                    Total_Sales_Amount = httpRequest.Form.Get("Total_Sales_Amount"),
                    PL_Amount = httpRequest.Form.Get("PL_Amount")

                };
                SqlCommand cmd = new SqlCommand("sp_PO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_PO");
                cmd.Parameters.AddWithValue("@PO_Number", po.PO_Number);
                cmd.Parameters.AddWithValue("@Vendor_Id", po.Vendor_Id);
                cmd.Parameters.AddWithValue("@Client_ID", po.Client_ID);
                cmd.Parameters.AddWithValue("@PO_Date", po.PO_Date);
                cmd.Parameters.AddWithValue("@Manager", po.Manager);
                cmd.Parameters.AddWithValue("@Vendor_Email", po.Vendor_Email);
                cmd.Parameters.AddWithValue("@LAN", po.LAN);
                cmd.Parameters.AddWithValue("@PromoCode", po.PromoCode);
                cmd.Parameters.AddWithValue("@SDF_Order", po.SDF_Order);
                cmd.Parameters.AddWithValue("@MS_Account_Manager", po.MS_Account_Manager);
                cmd.Parameters.AddWithValue("@Payment_InDays", po.Payment_InDays);
                cmd.Parameters.AddWithValue("@Delivery_In_Days", po.Delivery_In_Days);
                cmd.Parameters.AddWithValue("@Delivery_Mode", po.Delivery_Mode);
                cmd.Parameters.AddWithValue("@MPN_ID", po.MPN_ID);
                cmd.Parameters.AddWithValue("@AEP_Authorization_No", po.AEP_Authorization_No);
                cmd.Parameters.AddWithValue("@DomainID", po.DomainID);
                cmd.Parameters.AddWithValue("@CDC_Discount", po.CDC_Discount);
                cmd.Parameters.AddWithValue("@CDC_Discount_Amount", po.CDC_Discount_Amount);
                cmd.Parameters.AddWithValue("@After_Discount_Amount", po.After_Discount_Amount);
                cmd.Parameters.AddWithValue("@IGST_Tax", po.IGST_Tax);
                cmd.Parameters.AddWithValue("@Tax_Amount", po.Tax_Amount);
                cmd.Parameters.AddWithValue("@After_Tax_Amount", po.After_Tax_Amount);
                cmd.Parameters.AddWithValue("@CN", po.CN);
                cmd.Parameters.AddWithValue("@CN_Amount", po.CN_Amount);
                cmd.Parameters.AddWithValue("@Grand_Total_Amount", po.Grand_Total_Amount);
                cmd.Parameters.AddWithValue("@Purchase_Amount", po.Purchase_Amount);
                cmd.Parameters.AddWithValue("@Total_Sales_Amount", po.Total_Sales_Amount);
                cmd.Parameters.AddWithValue("@PL_Amount", po.PL_Amount);
                cmd.Parameters.AddWithValue("@Id", po.id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpPut]
        //Update : api/ManagePO/1
        public IHttpActionResult UpdatePo(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var po = new manage_po()
                {
                    PO_Number = httpRequest.Form.Get("PO_Number"),
                    Vendor_Id = httpRequest.Form.Get("Vendor_Id"),
                    Client_ID = httpRequest.Form.Get("Client_ID"),
                    PO_Date = httpRequest.Form.Get("PO_Date"),
                    Manager = httpRequest.Form.Get("Manager"),
                    Vendor_Email = httpRequest.Form.Get("Vendor_Email"),
                    LAN = httpRequest.Form.Get("LAN"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    SDF_Order = httpRequest.Form.Get("SDF_Order"),
                    MS_Account_Manager = httpRequest.Form.Get("MS_Account_Manager"),
                    Payment_InDays = httpRequest.Form.Get("Payment_InDays"),
                    Delivery_In_Days = httpRequest.Form.Get("Delivery_In_Days"),
                    Delivery_Mode = httpRequest.Form.Get("Delivery_Mode"),
                    MPN_ID = httpRequest.Form.Get("MPN_ID"),
                    AEP_Authorization_No = httpRequest.Form.Get("AEP_Authorization_No"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    CDC_Discount = httpRequest.Form.Get("CDC_Discount"),
                    CDC_Discount_Amount = httpRequest.Form.Get("CDC_Discount_Amount"),
                    After_Discount_Amount = httpRequest.Form.Get("After_Discount_Amount"),
                    IGST_Tax = httpRequest.Form.Get("IGST_Tax"),
                    Tax_Amount = httpRequest.Form.Get("Tax_Amount"),
                    After_Tax_Amount = httpRequest.Form.Get("After_Tax_Amount"),
                    CN = httpRequest.Form.Get("CN"),
                    CN_Amount = httpRequest.Form.Get("CN_Amount"),
                    Grand_Total_Amount = httpRequest.Form.Get("Grand_Total_Amount"),
                    Purchase_Amount = httpRequest.Form.Get("Purchase_Amount"),
                    Total_Sales_Amount = httpRequest.Form.Get("Total_Sales_Amount"),
                    PL_Amount = httpRequest.Form.Get("PL_Amount")

                };
                SqlCommand cmd = new SqlCommand("sp_PO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updatePObyId");
                cmd.Parameters.AddWithValue("@PO_Number", po.PO_Number);
                cmd.Parameters.AddWithValue("@Vendor_Id", po.Vendor_Id);
                cmd.Parameters.AddWithValue("@Client_ID", po.Client_ID);
                cmd.Parameters.AddWithValue("@PO_Date", po.PO_Date);
                cmd.Parameters.AddWithValue("@Manager", po.Manager);
                cmd.Parameters.AddWithValue("@Vendor_Email", po.Vendor_Email);
                cmd.Parameters.AddWithValue("@LAN", po.LAN);
                cmd.Parameters.AddWithValue("@PromoCode", po.PromoCode);
                cmd.Parameters.AddWithValue("@SDF_Order", po.SDF_Order);
                cmd.Parameters.AddWithValue("@MS_Account_Manager", po.MS_Account_Manager);
                cmd.Parameters.AddWithValue("@Payment_InDays", po.Payment_InDays);
                cmd.Parameters.AddWithValue("@Delivery_In_Days", po.Delivery_In_Days);
                cmd.Parameters.AddWithValue("@Delivery_Mode", po.Delivery_Mode);
                cmd.Parameters.AddWithValue("@MPN_ID", po.MPN_ID);
                cmd.Parameters.AddWithValue("@AEP_Authorization_No", po.AEP_Authorization_No);
                cmd.Parameters.AddWithValue("@DomainID", po.DomainID);
                cmd.Parameters.AddWithValue("@CDC_Discount", po.CDC_Discount);
                cmd.Parameters.AddWithValue("@CDC_Discount_Amount", po.CDC_Discount_Amount);
                cmd.Parameters.AddWithValue("@After_Discount_Amount", po.After_Discount_Amount);
                cmd.Parameters.AddWithValue("@IGST_Tax", po.IGST_Tax);
                cmd.Parameters.AddWithValue("@Tax_Amount", po.Tax_Amount);
                cmd.Parameters.AddWithValue("@After_Tax_Amount", po.After_Tax_Amount);
                cmd.Parameters.AddWithValue("@CN", po.CN);
                cmd.Parameters.AddWithValue("@CN_Amount", po.CN_Amount);
                cmd.Parameters.AddWithValue("@Grand_Total_Amount", po.Grand_Total_Amount);
                cmd.Parameters.AddWithValue("@Purchase_Amount", po.Purchase_Amount);
                cmd.Parameters.AddWithValue("@Total_Sales_Amount", po.Total_Sales_Amount);
                cmd.Parameters.AddWithValue("@PL_Amount", po.PL_Amount);
                cmd.Parameters.AddWithValue("@Id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpDelete]
        //Delete : api/ManagePO/1
        public IHttpActionResult DeletePo(string id)
        {
            db.delete_PO(id);
            return Ok("Success");
        }
    }
    public class ManageQuotationController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        macreel_setup.Models.admin.DataServices db = new macreel_setup.Models.admin.DataServices();
        [HttpGet]
        // Get : api/ManageQuotation
        public IHttpActionResult Get_QuotationlistGeneratedBy(string lodinId)
        {
            List<manage_quotation> quotlist = new List<manage_quotation>();
            quotlist = db.getQuotationlistGeneratedBy(lodinId);
            return Ok(quotlist);
        }
        [HttpGet]
        // Get : api/ManageQuotation
        public IHttpActionResult Get_Quotationlistwithversion(string LeadId)
        {
            List<manage_quotation> quotlist = new List<manage_quotation>();
            quotlist = db.getQuotationlistwithversion(LeadId);
            return Ok(quotlist);
        }
        [HttpGet]
        // Get : api/ManageQuotation/1
        public IHttpActionResult Get_QuotationbyId(string id)
        {
            manage_quotation quot = new manage_quotation();
            quot = db.getQuotationbyId(id);
            return Ok(quot);
        }
        [HttpPost]
        //Insert : api/ManageQuotation
        public IHttpActionResult Insert_Quotation()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var quotation = new manage_quotation()
                {
                    Quotation_No = httpRequest.Form.Get("Quotation_No"),
                    Lead_Reference = httpRequest.Form.Get("Lead_Reference"),
                    Client_Name = httpRequest.Form.Get("Client_Name"),
                    Contact_Person_No = httpRequest.Form.Get("Contact_Person_No"),
                    Email = httpRequest.Form.Get("Email"),
                    Company_Number = httpRequest.Form.Get("Company_Number"),
                    Address = httpRequest.Form.Get("Address"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    Select_Bill_TO = httpRequest.Form.Get("Select_Bill_TO"),
                    Date = httpRequest.Form.Get("Date"),
                    Select_Ship_TO = httpRequest.Form.Get("Select_Ship_TO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LAN_ID = httpRequest.Form.Get("LAN_ID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAX_Amount = httpRequest.Form.Get("TAX_Amount"),
                    Price_Before_TAX = httpRequest.Form.Get("Price_Before_TAX"),
                    Price_After_TAX = httpRequest.Form.Get("Price_After_TAX"),
                    Generated_Date = httpRequest.Form.Get("Generated_Date"),
                    GeneratedBy = httpRequest.Form.Get("GeneratedBy")

                };
                SqlCommand cmd = new SqlCommand("sp_Quotation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "insert_quotation");
                cmd.Parameters.AddWithValue("@Quotation_No", quotation.Quotation_No);
                cmd.Parameters.AddWithValue("@Lead_Reference", quotation.Lead_Reference);
                cmd.Parameters.AddWithValue("@Client_Name", quotation.Client_Name);
                cmd.Parameters.AddWithValue("@Contact_Person_No", quotation.Contact_Person_No);
                cmd.Parameters.AddWithValue("@Email", quotation.Email);
                cmd.Parameters.AddWithValue("@Company_Number", quotation.Company_Number);
                cmd.Parameters.AddWithValue("@Address", quotation.Address);
                cmd.Parameters.AddWithValue("@Employee", quotation.Employee);
                cmd.Parameters.AddWithValue("@Branch", quotation.Branch);
                cmd.Parameters.AddWithValue("@Select_Bill_TO", quotation.Select_Bill_TO);
                cmd.Parameters.AddWithValue("@Date", quotation.Date);
                cmd.Parameters.AddWithValue("@Select_Ship_TO", quotation.Select_Ship_TO);
                cmd.Parameters.AddWithValue("@Add_PORNo", quotation.Add_PORNo);
                cmd.Parameters.AddWithValue("@LAN_ID", quotation.LAN_ID);
                cmd.Parameters.AddWithValue("@PromoCode", quotation.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", quotation.DomainID);
                cmd.Parameters.AddWithValue("@InState_OutState", quotation.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", quotation.Tax);
                cmd.Parameters.AddWithValue("@TAX_Amount", quotation.TAX_Amount);
                cmd.Parameters.AddWithValue("@Price_Before_TAX", quotation.Price_Before_TAX);
                cmd.Parameters.AddWithValue("@Price_After_TAX", quotation.Price_After_TAX);
                cmd.Parameters.AddWithValue("@Generated_Date", quotation.Generated_Date);
                cmd.Parameters.AddWithValue("@GeneratedBy", quotation.GeneratedBy);
                cmd.Parameters.AddWithValue("@Id", quotation.id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpPut]
        //Update : api/ManageQuotation/1
        public IHttpActionResult UpdateQuotation(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var quotation = new manage_quotation()
                {
                    Quotation_No = httpRequest.Form.Get("Quotation_No"),
                    Lead_Reference = httpRequest.Form.Get("Lead_Reference"),
                    Client_Name = httpRequest.Form.Get("Client_Name"),
                    Contact_Person_No = httpRequest.Form.Get("Contact_Person_No"),
                    Email = httpRequest.Form.Get("Email"),
                    Company_Number = httpRequest.Form.Get("Company_Number"),
                    Address = httpRequest.Form.Get("Address"),
                    Employee = httpRequest.Form.Get("Employee"),
                    Branch = httpRequest.Form.Get("Branch"),
                    Select_Bill_TO = httpRequest.Form.Get("Select_Bill_TO"),
                    Date = httpRequest.Form.Get("Date"),
                    Select_Ship_TO = httpRequest.Form.Get("Select_Ship_TO"),
                    Add_PORNo = httpRequest.Form.Get("Add_PORNo"),
                    LAN_ID = httpRequest.Form.Get("LAN_ID"),
                    PromoCode = httpRequest.Form.Get("PromoCode"),
                    DomainID = httpRequest.Form.Get("DomainID"),
                    InState_OutState = httpRequest.Form.Get("InState_OutState"),
                    Tax = httpRequest.Form.Get("Tax"),
                    TAX_Amount = httpRequest.Form.Get("TAX_Amount"),
                    Price_Before_TAX = httpRequest.Form.Get("Price_Before_TAX"),
                    Price_After_TAX = httpRequest.Form.Get("Price_After_TAX"),
                    Generated_Date = httpRequest.Form.Get("Generated_Date"),
                    GeneratedBy = httpRequest.Form.Get("GeneratedBy")

                };
                SqlCommand cmd = new SqlCommand("sp_Quotation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "updateQuotationById");
                cmd.Parameters.AddWithValue("@Quotation_No", quotation.Quotation_No);
                cmd.Parameters.AddWithValue("@Lead_Reference", quotation.Lead_Reference);
                cmd.Parameters.AddWithValue("@Client_Name", quotation.Client_Name);
                cmd.Parameters.AddWithValue("@Contact_Person_No", quotation.Contact_Person_No);
                cmd.Parameters.AddWithValue("@Email", quotation.Email);
                cmd.Parameters.AddWithValue("@Company_Number", quotation.Company_Number);
                cmd.Parameters.AddWithValue("@Address", quotation.Address);
                cmd.Parameters.AddWithValue("@Employee", quotation.Employee);
                cmd.Parameters.AddWithValue("@Branch", quotation.Branch);
                cmd.Parameters.AddWithValue("@Select_Bill_TO", quotation.Select_Bill_TO);
                cmd.Parameters.AddWithValue("@Date", quotation.Date);
                cmd.Parameters.AddWithValue("@Select_Ship_TO", quotation.Select_Ship_TO);
                cmd.Parameters.AddWithValue("@Add_PORNo", quotation.Add_PORNo);
                cmd.Parameters.AddWithValue("@LAN_ID", quotation.LAN_ID);
                cmd.Parameters.AddWithValue("@PromoCode", quotation.PromoCode);
                cmd.Parameters.AddWithValue("@DomainID", quotation.DomainID);
                cmd.Parameters.AddWithValue("@InState_OutState", quotation.InState_OutState);
                cmd.Parameters.AddWithValue("@Tax", quotation.Tax);
                cmd.Parameters.AddWithValue("@TAX_Amount", quotation.TAX_Amount);
                cmd.Parameters.AddWithValue("@Price_Before_TAX", quotation.Price_Before_TAX);
                cmd.Parameters.AddWithValue("@Price_After_TAX", quotation.Price_After_TAX);
                cmd.Parameters.AddWithValue("@Generated_Date", quotation.Generated_Date);
                cmd.Parameters.AddWithValue("@GeneratedBy", quotation.GeneratedBy);
                cmd.Parameters.AddWithValue("@Id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting data: " + ex.Message);
            }
        }
        [HttpDelete]
        //Update : api/ManageQuotation/1
        public IHttpActionResult DeleteQuotation(string id)
        {
            db.delete_Quotation(id);
            return Ok("Success");
        }
    }
}

