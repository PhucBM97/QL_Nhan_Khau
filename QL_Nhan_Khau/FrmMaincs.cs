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
    public partial class FrmMaincs : Form
    {
        private static bool _exiting;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        public FrmMaincs(String userName)
        {
            InitializeComponent();
            lbName.Text = userName;
        }

        private void FrmMaincs_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
            //
            try
            {
                conn = new SqlConnection($"Data Source=LAPTOP-HED5RGQJ;Initial Catalog=QLNK;User ID=sa; Password=123456");
                conn.Open();
            }
            catch(Exception)
            {
                MessageBox.Show("Khong ket noi duoc CSDL", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable DSNhanKhau;
            DSNhanKhau = LoadDuLieu();
            DSNK_ListView(DSNhanKhau);
            // load combo SHK
            SqlDataAdapter da1 = new SqlDataAdapter();
            SqlCommand cmd1 = new SqlCommand("select * from SoHoKhau", conn);
            da1.SelectCommand = cmd1;
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "SoHoKhau");
            cb_shk.DataSource = ds1;
            cb_shk.DisplayMember = "SoHoKhau.MaSHK";
            cb_shk.ValueMember = "SoHoKhau.MaSHK";
            // load combo tam tru
            SqlDataAdapter da2 = new SqlDataAdapter();
            SqlCommand cmd2 = new SqlCommand("select * from SoTamTru", conn);
            da2.SelectCommand = cmd2;
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "SoTamTru");
            cb_tamtru.DataSource = ds2;
            cb_tamtru.DisplayMember = "SoTamTru.MaSTT";
            cb_tamtru.ValueMember = "SoTamTru.MaSTT";
            //
            SqlDataAdapter da3 = new SqlDataAdapter();
            SqlCommand cmd3 = new SqlCommand("select * from GiayTamVang", conn);
            da3.SelectCommand = cmd3;
            DataSet ds3 = new DataSet();
            da3.Fill(ds3, "GiayTamVang");
            cb_tamvang.DataSource = ds3;
            cb_tamvang.DisplayMember = "GiayTamVang.MaGTV";
            cb_tamvang.ValueMember = "GiayTamVang.MaGTV";
            //
            conn.Close();



        }
        DataTable LoadDuLieu()
        {
            da = new SqlDataAdapter($"select Socmnd,TenNK,Ngaysinh,gioitinh,Diachi,sodt,MaSHK,MaGTV,MaSTT from NhanKhau", conn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        private void DSNK_ListView(DataTable tbNhanKhau)
        {
            ListViewItem itemm;
            listView1.Items.Clear();
            for(int i = 0; i < tbNhanKhau.Rows.Count; i++)
            {
                itemm = listView1.Items.Add(tbNhanKhau.Rows[i][0].ToString());
                for(int j = 1; j < tbNhanKhau.Columns.Count; j++)
                {
                    itemm.SubItems.Add(tbNhanKhau.Rows[i][j].ToString());
                }
            }
        }
        private void DSNK_ListView1(DataTable tbNhanKhau)
        {
            ListViewItem itemm;
            listView1.Items.Clear();
            for(int i = 0; i < tbNhanKhau.Rows.Count; i++)
            {
                itemm = listView1.Items.Add(tbNhanKhau.Rows[i][0].ToString());
                for(int j = 1; j < tbNhanKhau.Columns.Count; j++)
                {
                    itemm.SubItems.Add(tbNhanKhau.Rows[i][j].ToString());
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Form1 fr1 = new Form1();
            fr1.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_ten.Clear();
            txt_id.Focus();
            txt_diachi.Clear();
            cb_shk.Text = "";
            txt_id.Clear();
            txt_sdt.Clear();
            chk_gt.Checked = false;
            cb_tamtru.Text = "";
            cb_tamvang.Text = "";

        }
        private void kiemTra()
        {
            if(string.IsNullOrEmpty(txt_ten.Text))
            {
                MessageBox.Show("Chưa nhập tên");
            }
            else if(string.IsNullOrEmpty(txt_diachi.Text))
            {
                MessageBox.Show("Chưa nhập địa chỉ");
            }
            else if(string.IsNullOrEmpty(cb_shk.Text))
            {
                MessageBox.Show("Chưa chọn hộ khẩu");
            }
            else if(string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Chưa nhập CMND");
            }
            else if(string.IsNullOrEmpty(txt_sdt.Text))
            {
                MessageBox.Show("Chưa nhập số điện thoại");
            }
            else if(chk_gt.Checked == false)
            {
                MessageBox.Show("Chưa chọn giới tính");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kiemTra();
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MessageBox.Show("Lưu thành công");
            }
            string gioitinh = chk_gt.Checked ? "Nam" : "Nữ";
            string Sql = $"insert into NhanKhau values('{txt_id.Text.ToString()}', '{txt_ten.Text.ToString()}', '{datepicker.Value.ToString("yyyy-MM-dd")}', '{gioitinh}', '{txt_diachi.Text.ToString()}', {Single.Parse(txt_sdt.Text.Trim().ToString())}, '{cb_shk.Text.ToString()}', '{cb_tamvang.Text.ToString()}', '{cb_tamtru.Text.ToString()}')";
            try
            {

                cmd = new SqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();

                DataTable tbNhanKhau;
                tbNhanKhau = LoadDuLieu();
                DSNK_ListView(tbNhanKhau);
            }
            catch(Exception)
            {

                MessageBox.Show("Lỗi khi lưu");
            }
            finally
            {
                conn.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                string idnk = listView1.SelectedItems[0].SubItems[0].Text;
                txt_id.Text = idnk;
                string tennk = listView1.SelectedItems[0].SubItems[1].Text;
                txt_ten.Text = tennk;
                string ngaysinhne = listView1.SelectedItems[0].SubItems[2].Text;
                datepicker.Text = ngaysinhne;
                string gioitinhne = listView1.SelectedItems[0].SubItems[3].Text;
                if(gioitinhne == "Nam")
                {
                    chk_gt.Checked = true;
                }
                else
                {
                    chk_gt.Checked = false;
                }
                string diachi = listView1.SelectedItems[0].SubItems[4].Text;
                txt_diachi.Text = diachi;
                string sdt = listView1.SelectedItems[0].SubItems[5].Text;
                txt_sdt.Text = sdt;
                string mashk = listView1.SelectedItems[0].SubItems[6].Text;
                cb_shk.Text = mashk;
                string magtv = listView1.SelectedItems[0].SubItems[7].Text;
                cb_tamvang.Text = magtv;
                string mastt = listView1.SelectedItems[0].SubItems[8].Text;
                cb_tamtru.Text = mastt;

            }
            else
            {
                txt_id.Text = string.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kiemTra();
            if(conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                    string gioitinh = chk_gt.Checked ? "Nam" : "Nữ";
                    string Sql = $"Update NhanKhau SET socmnd = '{txt_id.Text.ToString()}', TenNK = '{txt_ten.Text.ToString()}', Ngaysinh = '{datepicker.Value.ToString("yyyy/MM/dd")}', gioitinh = '{gioitinh}', diachi = '{txt_diachi.Text.ToString()}', sodt = '{txt_sdt.Text.ToString()}', MaSHK = '{cb_shk.Text.ToString()}', MaGTV = '{cb_tamvang.Text.ToString()}', MaSTT = '{cb_tamtru.Text.ToString()}' where socmnd = '{txt_id.Text.ToString()}' ";
                    cmd = new SqlCommand(Sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công");

                }
                catch(Exception)
                {
                    
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
                try
                {
                    string sql = $"delete from NhanKhau where Socmnd = '{txt_id.Text.ToString()}'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    //
                    DataTable tbNhanKhau;
                    tbNhanKhau = LoadDuLieu();
                    DSNK_ListView(tbNhanKhau);

                }
                catch(Exception)
                {
                    MessageBox.Show("Lỗi khi xóa");
                }
                finally
                {
                    conn.Close();
                }
            }
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_id.Text))
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm");
            }
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if(txt_id.Text != "")
                    {
                        foreach(ListViewItem item in listView1.Items)
                        {
                            if(item.Text.ToLower().Trim().Contains(txt_id.Text.ToLower().Trim()))
                            {
                                item.Selected = true;
                                item.BackColor = SystemColors.Highlight;
                                item.ForeColor = SystemColors.HighlightText;
                            }
                            else
                            {
                                listView1.Items.Remove(item);
                            }
                        }
                    }
                    if(listView1.SelectedItems.Count == 1)
                    {
                        listView1.Focus();
                    }
                }
                else
                {
                    DataTable tbNhanKhau;
                    tbNhanKhau = LoadDuLieu();
                    DSNK_ListView(tbNhanKhau);
                }
            }
            catch(Exception )
            {

                MessageBox.Show("Lỗi");
            }
            finally
            {
                conn.Close();
            }

            //string Sql = $"select socmnd,tenNK,ngaysinh,diachi,sodt,maSHK,maGTV,maSTT from NhanKhau where Socmnd = '{txt_id.Text.ToString()}'";
            //try
            //{

            //    cmd = new SqlCommand(Sql, conn);
            //    cmd.ExecuteScalar();
            //    //
            //    DataTable tbNhanKhau;
            //    tbNhanKhau = LoadDuLieu();
            //    DSNK_ListView(tbNhanKhau);
            //}
            //catch(Exception)
            //{

            //    MessageBox.Show("Lỗi");
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable tbNhanKhau;
            tbNhanKhau = LoadDuLieu();
            DSNK_ListView(tbNhanKhau);
        }
        
        private void button6_Click_1(object sender, EventArgs e)
        {
            if(!_exiting && MessageBox.Show("Bạn có chắc muốn thoát?",
                                  "Thông Báo !",
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information) == DialogResult.OK)
            {
                _exiting = true;
                Environment.Exit(1);
            }
        }
    }
}
