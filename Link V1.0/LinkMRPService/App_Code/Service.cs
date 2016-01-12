using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Text;
using System.Data;
using BusinessHandler;
using EntityHandler;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class Service
{
    BALPayment objBALPayment = new BALPayment();

    [OperationContract]
    public List<LINKPayment> GetCreditorFinal(LINKPayment objPayment)
    {
        return objBALPayment.BALGetFinalCreditor(objPayment);
    }

    [OperationContract]
    public List<LINKPayment> GetCreditorFinalSupplierList()
    {
        return objBALPayment.BALGetFinalCreditorSupplierList();
    }

    [OperationContract]
    public List<LINKPayment> GetCreditorFinalSupplier(LINKPayment objPayment)
    {
        return objBALPayment.BALGetFinalSupplier(objPayment);
    }

    [OperationContract]
    public List<LINKPayment> GetCreditorFinalSupplierByID(LINKPayment objPayment)
    {
        return objBALPayment.BALGetFinalSupplierByID(objPayment);
    }

    [OperationContract]
    public List<LINKPayment> GetCreditorFinalGRN(LINKPayment objPayment)
    {
        return objBALPayment.BALGetFinalCreditorGRN(objPayment);
    }

    // Pahan Sri Madusanka Rodrigo
    BusinessHandler.CustomerMaster objCustomerMaster = new BusinessHandler.CustomerMaster();

    [OperationContract]
    public List<EntityHandler.CustomerMaster> GetCustomerMaster()
    {
        return objCustomerMaster.BALGetCustomerMaster();
    }
}
