using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QLNK
{
    public partial class Form1 : Form
    {
        public SqlDataReader SqlDataReader { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection($"Data Source=LAPTOP-HED5RGQJ;Initial Catalog=QLNK;User ID=sa; Password=123456");
            try
            {
                
                string taikhoan = txtTen.Text;
                string matkhau = txtPass.Text;
                if(taikhoan == null || taikhoan.Equals(""))
                {
                    MessageBox.Show("Chưa nhập tên tài khoản");
                }
                if(matkhau == null || matkhau.Equals(""))
                {
                    MessageBox.Show("Chưa nhập mật khẩu");
                }
                conn.Open();
                string sql = $"select * from users where TaiKhoan = '{taikhoan}' and MatKhau = '{matkhau}'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    MessageBox.Show("Thành Công");
                    FrmMaincs frM = new FrmMaincs(taikhoan);
                    frM.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Đăng Nhập thất bại");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Lỗi kết nối");
            }

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn có muốn thoát khônng", "Thông báo", MessageBoxButtons.OKCancel);
                if(tb == DialogResult.OK)
                Application.Exit();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
