using AddonTestCadel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddonTestCadel.Behaviour
{
    public class PagosBehaviour
    {
        public ComponentsPagos _components = new ComponentsPagos();

        public PagosBehaviour()
        {
            setEvent();
        }

        private void setEvent()
        {
            _components
                .btnAceptar
                 .ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(ClickAfter_btnGenerarPagos);
        }
    }
}
