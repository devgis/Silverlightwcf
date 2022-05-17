using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace SilverlightAPP
{
    public partial class AddInfo : ChildWindow
    {
        public AddInfo()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            
            #region 验证
            if (string.IsNullOrEmpty(tbDepID.Text.Trim()))
            {
                MessageBox.Show("DepID不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToInt32(tbDepID.Text);
                }
                catch
                {
                    MessageBox.Show("DepID必须为整数！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbOrgID.Text.Trim()))
            {
                MessageBox.Show("OrgID不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToInt32(tbOrgID.Text);
                }
                catch
                {
                    MessageBox.Show("OrgID必须为整数！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbResNO.Text.Trim()))
            {
                MessageBox.Show("ResNO不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(tbResName.Text.Trim()))
            {
                MessageBox.Show("ResName不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(tbResDesc.Text.Trim()))
            {
                MessageBox.Show("ResDesc不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(tbStatus.Text.Trim()))
            {
                MessageBox.Show("Status不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToInt32(tbStatus.Text);
                }
                catch
                {
                    MessageBox.Show("Status必须为整数！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbType.Text.Trim()))
            {
                MessageBox.Show("Type不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToInt32(tbType.Text);
                }
                catch
                {
                    MessageBox.Show("Type必须为整数！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbResType.Text.Trim()))
            {
                MessageBox.Show("ResType不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToInt32(tbResType.Text);
                }
                catch
                {
                    MessageBox.Show("ResType必须为整数！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbResGroupNo.Text.Trim()))
            {
                MessageBox.Show("ResGroupNo不能为空！");
                return;
            }
            #endregion

            #region 保存
            //查询id
            var c1 = new MyWS.DServiceClient();
            c1.GetMaxIDCompleted += (s1, e1) =>
            { // ExampleMethodAsync returns a Task. await ExampleMethodAsync();
                if (e1.Result>=0m)
                {
                    var res = new MyWS.Resource()
                    {
                        ResID = e1.Result+1,
                        DepID = Convert.ToDecimal(tbDepID.Text),
                        OrgID = Convert.ToDecimal(tbOrgID.Text),
                        IsDefault = cbIsDefault.IsChecked ?? false,
                        ResDesc = tbResDesc.Text,
                        ResGroupNo = tbResGroupNo.Text,
                        ResName = tbResName.Text,
                        ResNO = tbResNO.Text,
                        ResType = Convert.ToInt32(tbResType.Text),
                        Status = Convert.ToInt32(tbStatus.Text),
                        Type = Convert.ToInt32(tbType.Text)
                    };
                    var client = new MyWS.DServiceClient();
                    client.AddResourceCompleted += (s, ee) =>
                    { // ExampleMethodAsync returns a Task. await ExampleMethodAsync();
                        if (ee.Result)
                        {
                            MessageBox.Show("保存成功！");
                        }
                        else
                        {
                            MessageBox.Show("保存失败！");
                        }
                    };
                    client.AddResourceAsync(res);
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
            };
            c1.GetMaxIDAsync();

            #endregion
            /*
            if (string.IsNullOrEmpty(tbTitle.Text.Trim()))
            {
                MessageBox.Show("标题不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(tbAuthor.Text.Trim()))
            {
                MessageBox.Show("作者不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(tbContent.Text.Trim()))
            {
                MessageBox.Show("内容不能为空！");
                return;
            }

            Info info = new Info();
            info.ID = Guid.NewGuid().ToString();
            info.Time = DateTime.Now;
            info.Title = tbTitle.Text;
            info.Content = tbContent.Text;
            info.Author = tbAuthor.Text;

            MyService.DBServiceClient client = new DBServiceClient();
            client.AddInfoCompleted += Client_AddInfoCompleted;
            client.AddInfoAsync(info);
             * */

        }

        private void Client_AddResourceCompleted(object sender, MyWS.AddResourceCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /*
        private void Client_AddInfoCompleted(object sender, AddInfoCompletedEventArgs e)
        {
            if (e.Result)
            {
                MessageBox.Show("保存成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }
         * */

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
