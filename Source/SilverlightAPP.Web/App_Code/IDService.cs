using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.Data.Models;

// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDService”。
[ServiceContract]
public interface IDService
{
    [OperationContract]
    bool AddResource(Resource r);

    [OperationContract]
    bool EditResource(Resource r);

    [OperationContract]
    bool DelResource(int ResID);

    [OperationContract]
    bool DelResources(List<int> ids);

    [OperationContract]
    List<Resource> QueryResource(string Where);

    [OperationContract]
    decimal GetMaxID();
}
