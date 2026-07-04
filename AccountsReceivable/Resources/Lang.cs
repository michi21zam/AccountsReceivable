using System.Collections.Generic;
using System.Threading;

namespace AccountsReceivable.Resources
{
    /// <summary>
    /// Diccionario simple de traducciones EN/ES.
    /// Uso en vistas: @Lang.T("Back")
    /// El idioma activo se determina por Thread.CurrentThread.CurrentUICulture,
    /// el cual se establece en Global.asax.cs (Application_BeginRequest) leyendo la cookie "lang".
    /// </summary>
    public static class Lang
    {
        private static readonly Dictionary<string, string> En = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> Es = new Dictionary<string, string>();
        private static readonly Dictionary<string, Dictionary<string, string>> All =
            new Dictionary<string, Dictionary<string, string>>();

        static Lang()
        {
            // ---------- General / Layout ----------
            Add("BrandTitle", "Accounts Receivable", "Cuentas por Cobrar");
            Add("NavReceivables", "Receivables", "Cuentas por Cobrar");
            Add("NavCustomers", "Customers", "Clientes");
            Add("NavEmployees", "Employees", "Empleados");
            Add("Footer", "All rights reserved", "Todos los derechos reservados");
            Add("Back", "BACK", "REGRESAR");
            Add("Save", "SAVE", "GUARDAR");
            Add("Cancel", "Cancel", "Cancelar");
            Add("CreateNew", "CREATE NEW", "CREAR NUEVO");
            Add("Edit", "EDIT", "EDITAR");
            Add("YesDelete", "YES, DELETE", "SÍ, ELIMINAR");
            Add("ConfirmDeletion", "Confirm Deletion", "Confirmar Eliminación");
            Add("Actions", "Actions", "Acciones");
            Add("LabelPayments", "Payments", "Pagos");
            Add("LabelCustomers", "Customers", "Clientes");
            Add("LabelEmployees", "Employees", "Empleados");
            Add("LabelAR", "Accounts Receivable", "Cuentas por Cobrar");

            // ---------- Language selection ----------
            Add("LanguageEnglish", "English", "Inglés");
            Add("LanguageSpanish", "Spanish", "Español");

            // ---------- Campos comunes ----------
            Add("FieldAccountNumber", "Account Number", "Número de Cuenta");
            Add("FieldCustomer", "Customer", "Cliente");
            Add("FieldEmployee", "Employee", "Empleado");
            Add("FieldIssueDate", "Issue Date", "Fecha de Emisión");
            Add("FieldDueDate", "Due Date", "Fecha de Vencimiento");
            Add("FieldTotalAmount", "Total Amount", "Monto Total");
            Add("FieldDescription", "Description", "Descripción");
            Add("FieldPaidAmount", "Paid Amount", "Monto Pagado");
            Add("FieldBalance", "Balance", "Saldo");
            Add("FieldStatus", "Status", "Estado");
            Add("FieldPaymentDate", "Payment Date", "Fecha de Pago");
            Add("FieldAmount", "Amount", "Monto");
            Add("FieldNotes", "Notes", "Notas");
            Add("FieldCustomerCode", "Customer Code", "Código de Cliente");
            Add("FieldName", "Name", "Nombre");
            Add("FieldTaxId", "Tax Id", "Identificación Fiscal");
            Add("FieldEmail", "Email", "Correo Electrónico");
            Add("FieldPhone", "Phone", "Teléfono");
            Add("FieldCreditLimit", "Credit Limit", "Límite de Crédito");
            Add("FieldAddress", "Address", "Dirección");
            Add("FieldEmployeeCode", "Employee Code", "Código de Empleado");
            Add("FieldFirstName", "First Name", "Nombre");
            Add("FieldLastName", "Last Name", "Apellido");
            Add("FieldFullName", "Full Name", "Nombre Completo");
            Add("FieldPosition", "Position", "Puesto");
            Add("FieldHireDate", "Hire Date", "Fecha de Contratación");
            Add("FieldDateFrom", "Date From", "Fecha Desde");
            Add("FieldDateTo", "Date To", "Fecha Hasta");
            Add("SelectCustomer", "Select a customer", "Seleccione un cliente");
            Add("SelectEmployee", "Select an employee", "Seleccione un empleado");
            Add("AllCustomers", "All customers", "Todos los clientes");
            Add("AllEmployees", "All employees", "Todos los empleados");
            Add("All", "All", "Todos");

            // ---------- Columnas de tablas ----------
            Add("ColAccountNum", "Account #", "N.º Cuenta");
            Add("ColCode", "Code", "Código");
            Add("ColName", "Name", "Nombre");

            // ---------- Receivables ----------
            Add("TitleReviewReceivables", "Review accounts receivable", "Revisar cuentas por cobrar");
            Add("TitleCreateReceivable", "Create new receivable", "Crear nueva cuenta por cobrar");
            Add("TitleEditReceivable", "Edit receivable settings", "Editar configuración de cuenta");
            Add("TitleDeleteReceivable", "Delete receivable?", "¿Eliminar cuenta por cobrar?");
            Add("TitleReceivableDetails", "Details of account #", "Detalles de la cuenta #");
            Add("SectionReceivableInfo", "Receivable Information", "Información de la Cuenta");
            Add("ConfirmDeleteReceivableText", "Are you sure you want to delete the following receivable? This action cannot be undone.", "¿Está seguro de que desea eliminar la siguiente cuenta por cobrar? Esta acción no se puede deshacer.");
            Add("NoReceivablesFound", "No receivables found.", "No se encontraron cuentas por cobrar.");
            Add("PaymentHistoryTitle", "Payment History", "Historial de Pagos");
            Add("AddPayment", "ADD PAYMENT", "AGREGAR PAGO");
            Add("NoPaymentsRecorded", "No payments recorded yet.", "Aún no hay pagos registrados.");
            Add("ConfirmDeletePaymentJs", "Delete this payment?", "¿Eliminar este pago?");

            // ---------- Payments ----------
            Add("TitleAddPayment", "Add new payment", "Agregar nuevo pago");
            Add("TitleEditPayment", "Edit payment", "Editar pago");
            Add("TitleDeletePayment", "Delete payment?", "¿Eliminar pago?");
            Add("TitlePaymentDetails", "Details of payment", "Detalles del pago");
            Add("SectionPaymentInfo", "Payment Information", "Información del Pago");
            Add("ConfirmDeletePaymentText", "Are you sure you want to delete this payment? This action cannot be undone.", "¿Está seguro de que desea eliminar este pago? Esta acción no se puede deshacer.");

            // ---------- Customers ----------
            Add("TitleManageCustomers", "Manage customers", "Gestionar clientes");
            Add("TitleCreateCustomer", "Create new customer", "Crear nuevo cliente");
            Add("TitleEditCustomer", "Edit customer", "Editar cliente");
            Add("TitleDeleteCustomer", "Delete customer?", "¿Eliminar cliente?");
            Add("TitleDetailsOf", "Details of", "Detalles de");
            Add("SectionCustomerInfo", "Customer Information", "Información del Cliente");
            Add("ConfirmDeleteCustomerText", "Are you sure you want to delete this customer? This action cannot be undone.", "¿Está seguro de que desea eliminar este cliente? Esta acción no se puede deshacer.");
            Add("ConfirmDeleteCustomerJs", "Delete this customer?", "¿Eliminar este cliente?");
            Add("NoCustomersFound", "No customers found.", "No se encontraron clientes.");

            // ---------- Employees ----------
            Add("TitleManageEmployees", "Manage employees", "Gestionar empleados");
            Add("TitleCreateEmployee", "Create new employee", "Crear nuevo empleado");
            Add("TitleEditEmployee", "Edit employee", "Editar empleado");
            Add("TitleDeleteEmployee", "Delete employee?", "¿Eliminar empleado?");
            Add("SectionEmployeeInfo", "Employee Information", "Información del Empleado");
            Add("ConfirmDeleteEmployeeText", "Are you sure you want to delete this employee? This action cannot be undone.", "¿Está seguro de que desea eliminar este empleado? Esta acción no se puede deshacer.");
            Add("ConfirmDeleteEmployeeJs", "Delete this employee?", "¿Eliminar este empleado?");
            Add("NoEmployeesFound", "No employees found.", "No se encontraron empleados.");
            Add("ErrorExceedsCreditLimit",
                "Total Amount exceeds the customer's Credit Limit ({0}).",
                "El Monto Total supera el Límite de Crédito del cliente ({0}).");
            Add("AvailableCreditLimit", "Available credit limit:", "Límite de crédito disponible:");

            // ---------- Validaciones ----------
            Add("ErrorExceedsCreditLimit",
                "Total Amount exceeds the customer's Credit Limit ({0}).",
                "El Monto Total supera el Límite de Crédito del cliente ({0}).");
            Add("AvailableCreditLimit", "Available credit limit:", "Límite de crédito disponible:");
            Add("ConfirmDeleteGenericText",
                "Are you sure you want to delete this record? This action cannot be undone.",
                "¿Está seguro de que desea eliminar este registro? Esta acción no se puede deshacer.");

            All["en"] = En;
            All["es"] = Es;
        }

        private static void Add(string key, string en, string es)
        {
            En[key] = en;
            Es[key] = es;
        }

        public static string CurrentLang
        {
            get
            {
                var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                return All.ContainsKey(lang) ? lang : "en";
            }
        }

        public static string T(string key)
        {
            var dict = All[CurrentLang];
            string val;
            if (dict.TryGetValue(key, out val)) return val;
            if (En.TryGetValue(key, out val)) return val;
            return key;
        }
    }
}
