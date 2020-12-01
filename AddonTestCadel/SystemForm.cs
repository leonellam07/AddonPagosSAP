using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTestCadel.Behaviour;
using SAPbouiCOM;

namespace AddonTestCadel
{
    public class SystemForm
    {
        private PagosBehaviour _formPagos;

        public SystemForm()
        {
            AppContext.SetApplication();

            AppContext
                .SBOApplication
                .StatusBar
                .SetText("Iniciando Add-on de Pagos de Cadelga...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

            setEvents();
            setFilters();
        }

        private void setFilters()
        {
            SAPbouiCOM.EventFilter eventFilter;
            SAPbouiCOM.EventFilters eventFilters = new SAPbouiCOM.EventFilters();
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_KEY_DOWN);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_VISIBLE);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_CLOSE);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_GOT_FOCUS);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_DELETE);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_DATASOURCE_LOAD);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE);
            eventFilter = eventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE);

            eventFilter.AddEx("UDO_PAGO_FT_TRANS");
            AppContext.SBOApplication.SetFilter(eventFilters);
        }

        private void setEvents()
        {
            AppContext
                .SBOApplication
                .AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);

            AppContext
                .SBOApplication
                .ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(ItemEvent_Form);
        }

        private void ItemEvent_Form(string FormUID, ref ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (FormUID.Contains("UDO_PAGO_F_TRANS"))
                {
                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_VISIBLE && pVal.BeforeAction == false)
                    {
                        AppContext.ActivateFormIsOpen("UDO_PAGO_FT_TRANS");
                        SAPbouiCOM.Form form = AppContext.SBOApplication.Forms.ActiveForm;

                        if (form.Items.Count > 0) { _formPagos = new PagosBehaviour(); }
                    }

                    if (pVal.EventType == SAPbouiCOM.BoEventTypes.et_FORM_CLOSE && pVal.BeforeAction == false)
                    {
                        _formPagos = null;
                        GC.Collect();
                    }
                }

            }
            catch (Exception ex)
            {
                AppContext
                     .SBOApplication
                     .StatusBar
                     .SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);

                BubbleEvent = false;
            }
        }

        private void SBO_Application_AppEvent(BoAppEventTypes EventType)
        {
            if (EventType == SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition || EventType == SAPbouiCOM.BoAppEventTypes.aet_ShutDown)
            {
                AppContext
                   .SBOApplication
                   .StatusBar
                   .SetText("Finalizando Add-on de Pagos de Cadelga...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
