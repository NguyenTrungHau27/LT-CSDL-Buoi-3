using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BaiTHBuoi5
{
    public partial class Form1 : Form
    {
        private SqlConnection cn = null;
        //private SqlCommand cm = null;
        string strcn = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            strcn = "Server = .; Database = QLBanHang; Integrated Security = true;";
            cn = new SqlConnection(strcn);
            connec();
            dgv1.DataSource = getData();
            txtMaLoai.DataBindings.Add("Text", dgv1, "MaLoaiSP");
        }

        void connec()
        {
            try
            {
                if (cn != null && cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    //MessageBox.Show("Ket noi thanh cong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi \n\n" + ex.Message);
            }
            //Con may cai cath nua!
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            string strsql = "DELETE FROM LOAISP WHERE MaLoaiSP = " + txtMaLoai.Text;
            int sumDelete = 0;
            try
            {
                SqlCommand cm = new SqlCommand(strsql, cn);
                /*
                cm.CommandText = strsql;
                cm.Connection = cn;*/
                sumDelete = cm.ExecuteNonQuery();
                dgv1.DataSource = getData();
                MessageBox.Show("Đã xóa " + sumDelete.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnec();
            }
        }
        
        void Disconnec()
        {
            if (cn != null && cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }

        }

        private List<Object> getData()
        {
            connec();
            string sql = "SELECT * FROM LOAISP";
            List<Object> list = new List<object>();
            SqlCommand cm = new SqlCommand(sql, cn);
            SqlDataReader dr = cm.ExecuteReader();
            int id;
            string  tenSP;
            while (dr.Read())
            {
                id = dr.GetInt32(0);
                tenSP = dr.GetString(1);
                var pro = new
                {
                    MaLoaiSP = id,
                    TenLoaiSP = tenSP
                };
                list.Add(pro);
            }
            dr.Close();
            return list;
        }

        private void txtMaLoai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
