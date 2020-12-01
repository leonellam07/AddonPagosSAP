using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddonTestCadel
{
    public class AppContext
    {
        public static string ConnectionString { get; set; }
        public static SAPbouiCOM.Application SBOApplication { get; set; }
        public static SAPbobsCOM.Company SBOCompany { get; set; }

        public static string SBOError { get { return "Error (" + SBOCompany.GetLastErrorCode() + "): " + SBOCompany.GetLastErrorDescription(); } }

        //Inicializa aplicacion
        public static void SetApplication()
        {
            //Se obtiene string de conexion de Cliente SAP B1
            if (Environment.GetCommandLineArgs().Count() == 1) { throw new Exception("No se agregaron los parametros de conexión...", new Exception("No se encontro string de conexión SAP B1")); }
            ConnectionString = Environment.GetCommandLineArgs().GetValue(1).ToString();

            //Se realiza conexion 
            SAPbouiCOM.SboGuiApi client = new SAPbouiCOM.SboGuiApi();
            client.Connect(ConnectionString);
            SBOApplication = client.GetApplication(-1);

            //Se carga <<Company>> de aplicacion   
            SBOCompany = new SAPbobsCOM.Company();
            string cookies = SBOCompany.GetContextCookie();
            string connectionContext = SBOApplication.Company.GetConnectionContext(SBOCompany.GetContextCookie());
            SBOCompany.SetSboLoginContext(connectionContext);

            //Conexion con sociedad
            if (SBOCompany.Connect() != 0) { throw new Exception(SBOError); }
        }

        //Activa Form de SAP BO Client con atributo <<formId>>
        public static bool ActivateFormIsOpen(string formID)
        {

            if (SBOApplication == null) { throw new Exception("Interfaz de la aplicación nula"); }
            if (SBOApplication.Forms.Count == 0) { throw new Exception("No se encontraron formularios"); }

            for (int x = 0; x < SBOApplication.Forms.Count; x++)
            {
                if (SBOApplication.Forms.Item(x).TypeEx == formID)
                {
                    SBOApplication.Forms.Item(x).Select();
                    return true;
                }
            }

            return false;
        }
    }
}
