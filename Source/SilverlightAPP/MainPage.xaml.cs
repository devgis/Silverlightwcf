using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightAPP
{
    public partial class MainPage : UserControl
    {
        bool cbLoad = false;
        public MainPage()
        {
            InitializeComponent();
            RefreshData();
        }



        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddInfo frmAdd = new AddInfo();
            frmAdd.Closed += FrmAdd_Closed;
            frmAdd.Show();
        }

        private void FrmAdd_Closed(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btEditRow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Tag.ToString());
        }

        private void btDelRow_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32((sender as Button).Tag);
            var client = new MyWS.DServiceClient();
            client.DelResourceCompleted += (s, ee) =>
            {
                if (ee.Result)
                {
                    RefreshData();
                    MessageBox.Show("删除成功!");
                }
            };
            client.DelResourceAsync(ID);
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            string ID = (sender as Button).Tag.ToString();
            EditInfo frmEditInfo = new EditInfo(Convert.ToInt32(ID));
            frmEditInfo.Show();
            frmEditInfo.Closed+= (s, ee) =>
            {
                RefreshData();
            };
        }

        public void RefreshData()
        {
            listIDS = new List<int>();
            string groupNo = string.Empty;
            if (cbResGroupNo.SelectedItem != null)
            {
                groupNo = cbResGroupNo.SelectedItem.ToString();
            }

            decimal resid = - 1;
            try
            {
                resid = Convert.ToDecimal(tbResID.Text);
            }
            catch
            { }

            //string where = string.Format("ResNO like '%{0}%' or ResName like '%{0}%' or ResDesc like '%{0}%' or ResGroupNo like '%{0}%'", tbWords.Text);
            //string where = string.Format("ResName like '%{2}%' or ResID = {1} or ResGroupNo = '%{0}%'" , groupNo, resid, tbResName.Text);

            string where = "1=1";
            if (cbResGroupNo.SelectedItem != null&&!"请选择".Equals(cbResGroupNo.SelectedItem.ToString()))
            {
                where += string.Format(" and ResGroupNo = '{0}'", cbResGroupNo.SelectedItem.ToString());
            }
            if (!string.IsNullOrEmpty(tbResID.Text))
            {
                where += string.Format(" and ResID = {0}", resid);
            }
            if (!string.IsNullOrEmpty(tbResName.Text))
            {
                where += string.Format(" and ResName like '%{0}%'", tbResName.Text);
            }

            var client = new MyWS.DServiceClient();
            client.QueryResourceCompleted += Client_QueryResourceCompleted;
            client.QueryResourceAsync(where);
        }

        private void Client_QueryResourceCompleted(object sender, MyWS.QueryResourceCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                /*
                myDataList.ItemsSource = e.Result;
                */

                PagedCollectionView pcv = new PagedCollectionView(e.Result);
                this.myDataList.ItemsSource = pcv;
                this.myPager.DataContext = pcv;

                if (!cbLoad)
                {
                    cbResGroupNo.Items.Clear();
                    cbResGroupNo.Items.Add("请选择");
                    foreach (MyWS.Resource r in e.Result)
                    {
                        if (!cbResGroupNo.Items.Contains(r.ResGroupNo))
                        {
                            cbResGroupNo.Items.Add(r.ResGroupNo);
                        }
                    }
                    cbLoad = true;
                    if (cbResGroupNo.Items.Count > 0)
                    {
                        cbResGroupNo.SelectedItem = 0;
                    }
                }
            }
        }
        List<int> listIDS = new List<int>();
        private void btDeleteSelect_Click(object sender, RoutedEventArgs e)
        {
            if (listIDS != null && listIDS.Count > 0)
            {
                var client = new MyWS.DServiceClient();
                client.DelResourcesCompleted += (s, ee) =>
                {
                    if (ee.Result)
                    {
                        RefreshData();
                        MessageBox.Show("删除成功!");
                    }
                };

                System.Collections.ObjectModel.ObservableCollection<int> ois = new System.Collections.ObjectModel.ObservableCollection<int>();
                foreach (int i in listIDS)
                {
                    ois.Add(i);
                }

                client.DelResourcesAsync(ois);
            }
            else
            {
                MessageBox.Show("请选中!");
            }
        }

        private void cbSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            int id= Convert.ToInt32((sender as CheckBox).Tag);
            listIDS.Remove(id);
        }

        private void cbSelect_Checked(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as CheckBox).Tag);
            listIDS.Add(id);
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbResID.Text))
            {
                try
                {
                    Convert.ToDecimal(tbResID.Text);
                }
                catch
                {
                    MessageBox.Show("ResID必须为整数类型");
                    return;
                }
            }
            
            RefreshData();
        }
    }
}
