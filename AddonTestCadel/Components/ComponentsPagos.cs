using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddonTestCadel.Components
{
    public class ComponentsPagos
    {
        public static string FormID = "UDO_FT_PAGO_TRANS";
        public static string FormSubID = "UDO_F_PAGO_TRANS";

        public SAPbouiCOM.Form MainForm { get; set; }

        public ComponentsPagos()
        {
            AppContext.ActivateFormIsOpen(FormID);
            MainForm = AppContext.SBOApplication.Forms.ActiveForm;

            btnAceptar = MainForm.Items.Item("1").Specific;
            txtComentarios = MainForm.Items.Item("19_U_E").Specific;
            mtxDetalles = MainForm.Items.Item("0_U_G").Specific;
            dbDetalles = MainForm.DataSources.DBDataSources.Item("@PAGO_DET");

        }

        public EditText txtComentarios { get; set; }
        public Button btnAceptar { get; set; }
        public Matrix mtxDetalles { get; set; }
        public DBDataSource dbDetalles { get; set; }
    }
}
