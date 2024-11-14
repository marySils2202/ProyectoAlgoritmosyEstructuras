﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProyectoED.ListasDobles;

namespace ProyectoED.ProyectoDS
{
    public struct Employee
    {
        public string nombreE, apellidoE, direccionE;
        public int edadE, telefonoE;
    };
    public partial class RegistroOFC : Form
    {
        string nombre, apellido, direccion;
        private int cantidad, edad, telefono;
        private Stacks pilas;
        private ColaSimple simples;
        private CircularQueue colascirculares;
        private ListaSimple ListaSimple;
        private ListasDobles listaDoble;
        public RegistroOFC()
        {
            InitializeComponent();
            pilas = new Stacks();
            ListaSimple = new ListaSimple();
            listaDoble = new ListasDobles();
            rbCircularesColas.Enabled = true;
            rbStacks.Enabled = true;
            rbSimpleColas.Enabled = true;
            ControlesFormulario(false);

        }
        #region Creación y Selección de la Estructura
        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (!rbStacks.Checked && !rbSimpleColas.Checked && !rbCircularesColas.Checked)
            {
                MessageBox.Show("Por favor seleccione una estructura antes de crear el arreglo");
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad de empleados", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad de empleados debe ser un número mayor a 0", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            if (rbStacks.Checked)
            {

                pilas.maximo = cantidad;
                pilas.employees = new Employee[pilas.maximo];
                txtCantidad.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;


            }
            if (rbSimpleColas.Checked)
            {
                cantidad = int.Parse(txtCantidad.Text.Trim());
                simples = new ColaSimple(cantidad);
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = false;
                rbListasDobles.Enabled = false;
                rbListasSimples.Enabled = false;
                button2.Enabled = true;
                button1.Enabled = true;

            }
            if (rbCircularesColas.Checked)
            {
                rbSimpleColas.Enabled = false;
                rbStacks.Enabled = false;
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = false;
                rbListasDobles.Enabled = false;
                rbListasSimples.Enabled = false;
            }

            cantidad = int.Parse(txtCantidad.Text.Trim());
            ControlesFormulario(true);
            MessageBox.Show("Arreglo creado correctamente", "Aviso", MessageBoxButtons.OK);


        }

        private void rbListasSimples_CheckedChanged(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked)
            {
                ListaSimple = new ListaSimple();
                ControlesFormulario(true);
                btnInicio.Enabled = false;
                txtCantidad.Enabled = false;
                ActivarControles(true);
                DesactivarRadioButton(false);
                rbListasDobles.Enabled = false;
                MessageBox.Show("Lista Simple creada correctamente", "Aviso", MessageBoxButtons.OK);
            }
        }
        private void rbListasDobles_CheckedChanged(object sender, EventArgs e)
        {
            if (rbListasDobles.Checked)
            {
                listaDoble = new ListasDobles();
                ControlesFormulario(true);
                btnInicio.Enabled = false;
                txtCantidad.Enabled = false;
                ActivarControles(true);
                DesactivarRadioButton(false);
                rbListasSimples.Checked = false;
                MessageBox.Show("Lista Doble creada correctamente", "Aviso", MessageBoxButtons.OK);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            rbCircularesColas.Enabled = true;
            rbSimpleColas.Enabled = true;
            rbStacks.Enabled = true;
            txtCantidad.Text = string.Empty;
            cantidad = 0;
            txtCantidad.Enabled = true;
            btnInicio.Enabled = true;
            dgEmpleados.Rows.Clear();
        }

        private Employee CrearEmpleado()
        {
            return new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };
        }

        #endregion
        #region Controles
        private void DesactivarRadioButton(bool activar)
        {
            rbCircularesColas.Enabled = activar;
            rbSimpleColas.Enabled = activar;
            rbStacks.Enabled = activar;
            btnAgregar.Enabled = activar;
            btnEliminar.Enabled = activar;
            button1.Enabled = activar;
            button2.Enabled = activar;
        }
        private void ActivarControles(bool Activar)
        {

            btnAntesAgregar.Enabled = Activar;
            btnDespuesAgregar.Enabled = Activar;
            btnInicioAgregar.Enabled = Activar;
            btnFinalAgregar.Enabled = Activar;
            btnAntesEliminar.Enabled = Activar;
            btnDespuesEliminar.Enabled = Activar;
            btnInicioEliminar.Enabled = Activar;
            btnEliminar_X.Enabled = Activar;
            btnFinalEliminar.Enabled = Activar;
            txtReferenciaAgregar.Enabled = Activar;
            txtReferenciaEliminar.Enabled = Activar;
        }
        private void LimpiarControles()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }
        private void ControlesFormulario(bool activar)
        {
            txtNombre.Enabled = activar;
            txtApellido.Enabled = activar;
            txtDireccion.Enabled = activar;
            txtEdad.Enabled = activar;
            txtTelefono.Enabled = activar;
            txtBusqueda.Enabled = activar;
        }
        #endregion

        #region Validaciones
        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtEdad.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Debe completar los campos solicitados", "Aviso", MessageBoxButtons.OK);
                return false;
            }

            if (!int.TryParse(txtEdad.Text.Trim(), out edad) || edad < 18)
            {
                MessageBox.Show("El empleado debe ser mayor de 18 años y la edad debe ser un número válido.", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return false;
            }

            if (!int.TryParse(txtTelefono.Text.Trim(), out telefono) || telefono <= 0)
            {
                MessageBox.Show("El teléfono debe ser un número entero positivo.", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return false;
            }
            return true;
        }
        #endregion
        #region Pilas y Colas
        public void Agregar(string nombre, string apellido, string direccion, int telefono, int edad)
        {
            if (rbStacks.Checked)
            {
                if (pilas.Estallena())
                {
                    MessageBox.Show("La pila está llena", "Aviso", MessageBoxButtons.OK);
                    LimpiarControles();
                    return;
                }
                else
                {
                    pilas.employees[pilas.topePila] = CrearEmpleado();
                    dgEmpleados.Rows.Add(nombre, apellido, telefono, direccion, edad);
                    pilas.topePila++;
                    LimpiarControles();
                }
            }
        }
        #endregion
        #region Pilas y Colas

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                int.TryParse(txtTelefono.Text, out telefono);
                int.TryParse(txtEdad.Text, out edad);
                Agregar(txtNombre.Text, txtApellido.Text, txtDireccion.Text, telefono, edad);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (rbStacks.Checked)
            {
                if (pilas.EstaVacia())
                {
                    MessageBox.Show("No se puede eliminar, la pila está vacía.");
                    return;
                }
                else
                {
                    pilas.topePila--;
                    dgEmpleados.Rows.RemoveAt(pilas.topePila);
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (rbSimpleColas.Checked)
            {

                if (simples.EstaVacia())
                {
                    MessageBox.Show("La cola está vacía, no se pueden eliminar más empleados.");
                }
                else
                {
                    simples.Eliminar();
                    dgEmpleados.Rows.Clear();
                    for (int i = 0; i <= simples.final; i++)
                    {
                        dgEmpleados.Rows.Add(simples.employees[i].nombreE, simples.employees[i].apellidoE, simples.employees[i].telefonoE, simples.employees[i].direccionE, simples.employees[i].edadE);
                    }
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }
            else
            if (rbCircularesColas.Checked)
            {
                if (colascirculares.EstaVacia())
                {
                    MessageBox.Show("La cola circular está vacía.");
                    return;
                }
                colascirculares.Eliminar();
                int fila = colascirculares.frente;

                if (fila >= 0 && fila < dgEmpleados.Rows.Count)
                {
                    dgEmpleados.Rows.RemoveAt(fila);
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (rbSimpleColas.Checked)
            {
                if (ValidarFormulario())
                {
                    if (simples.EstaLlena())
                    {
                        MessageBox.Show("La cola está llena. No se puede agregar más empleados.");
                    }
                    else
                    {
                        Employee empleado = CrearEmpleado();
                        simples.Agregar(empleado);
                        dgEmpleados.Rows.Clear();
                        for (int i = 0; i <= simples.final; i++)
                        {
                            dgEmpleados.Rows.Add(simples.employees[i].nombreE, simples.employees[i].apellidoE, simples.employees[i].telefonoE, simples.employees[i].direccionE, simples.employees[i].edadE);
                        }
                        LimpiarControles();
                    }
                }

            }
            else
            if (rbCircularesColas.Checked)
            {
                if (!ValidarFormulario()) return;

                cantidad = int.Parse(txtCantidad.Text.Trim());
                colascirculares = new CircularQueue(cantidad);

                if (colascirculares.EstaLlena())
                {
                    MessageBox.Show("La cola está llena", "Aviso", MessageBoxButtons.OK);
                    LimpiarControles();
                    return;
                }
                Employee empleado = CrearEmpleado();
                colascirculares.Agregar(empleado);
                dgEmpleados.Rows.Add(
                    empleado.nombreE,
                    empleado.apellidoE,
                    empleado.telefonoE,
                    empleado.direccionE,
                    empleado.edadE
                );
                LimpiarControles();
            }
        }
        #endregion
        #region Listas Dobles y Enlazadas
        //Métodos para mostrar listas
        private void MostrarListaDoble()
        {
            dgEmpleados.Rows.Clear();
            NodoDoble actual = listaDoble.ObtenerCabeza();

            if (actual == null)
            {
                MessageBox.Show("La lista doble está vacía.");
                return;
            }

            while (actual != null)
            {
                dgEmpleados.Rows.Add(
                    actual.Empleado.nombreE,
                    actual.Empleado.apellidoE,
                    actual.Empleado.telefonoE,
                    actual.Empleado.direccionE,
                    actual.Empleado.edadE
                );
                actual = actual.Siguiente;
            }
        }
        private void MostrarListaSimple()
        {
            dgEmpleados.Rows.Clear();
            Nodo actual = ListaSimple.ObtenerCabeza();

            if (actual == null)
            {
                MessageBox.Show("La lista simple está vacía.");
                return;
            }

            while (actual != null)
            {
                dgEmpleados.Rows.Add(
                    actual.Empleado.nombreE,
                    actual.Empleado.apellidoE,
                    actual.Empleado.telefonoE,
                    actual.Empleado.direccionE,
                    actual.Empleado.edadE
                );
                actual = actual.Siguiente;
            }
        }
        //Funciones para agregar elementos a las listas
        private void btnInicioAgregar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked)
            {
                if (!ValidarFormulario()) return;
                Employee empleado = CrearEmpleado();
                ListaSimple.agregarInicio(empleado);
                MostrarListaSimple();
                LimpiarControles();
                MessageBox.Show("Registro agregado exitosamente.");
            }
            else
                if (rbListasDobles.Checked)
            {
                if (!ValidarFormulario()) return;
                Employee empleado = CrearEmpleado();
                listaDoble.AgregarInicio(empleado);
                MostrarListaDoble();
                LimpiarControles();
                MessageBox.Show("Registro agregado exitosamente.");
            }

        }
        private void btnAntesAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAgregar.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAgregar.Focus();
                return;
            }
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarAntes(empleado, txtReferenciaAgregar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarAntes(empleado, txtReferenciaAgregar.Text);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }
        private void btnDespuesAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAgregar.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAgregar.Focus();
                return;
            }
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarDespues(empleado, txtReferenciaAgregar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarDespues(empleado, txtReferenciaAgregar.Text);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }
        private void btnFinalAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarFinal(empleado);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarFinal(empleado);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }

        //Funciones para eliminar objetos de las listas
        private void btnEliminar_X_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
            {
                MessageBox.Show("Ingrese el nombre del empleado como referencia para eliminar el registro");
                txtReferenciaEliminar.Focus();
                return;
            }
            if (rbListasSimples.Checked)
            {
                if (ListaSimple.ListaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar.");
                    return;
                }
                ListaSimple.Eliminar_X(txtReferenciaEliminar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar.");
                    return;
                }
                listaDoble.Eliminar_X(txtReferenciaEliminar.Text);
                MostrarListaDoble();
            }
            txtReferenciaEliminar.Text = string.Empty;
            MessageBox.Show("Registro eliminado exitosamente");
        }
        private void btnInicioEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                }
                else
                {
                    ListaSimple.EliminarInicio();
                    MostrarListaSimple();
                    MessageBox.Show("Registro Eliminado Exitosamente");

                }
            }
            else if (rbListasDobles.Checked == true)
            {
                listaDoble.EliminarInicio();
                MostrarListaDoble();
                MessageBox.Show("Registro Eliminado Exitosamente");

            }

        }
        private void btnFinalEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                }
                else
                {
                    ListaSimple.EliminarFinal();
                    MostrarListaSimple();
                    MessageBox.Show("Registro Eliminado Exitosamente");

                }
            }
            else if (rbListasDobles.Checked == true)
            {
                listaDoble.EliminarFinal();
                listaDoble.MostrarLista(dgEmpleados);
                MessageBox.Show("Registro Eliminado Exitosamente");

            }

        }
        private void btnAntesEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                ListaSimple.EliminarAntes(txtReferenciaEliminar.Text);
                MostrarListaSimple();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }
            else
            if (rbListasDobles.Checked == true)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                listaDoble.EliminarAntes(txtReferenciaEliminar.Text);
                MostrarListaDoble();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }
        }
        private void btnDespuesEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                ListaSimple.EliminarDespues(txtReferenciaEliminar.Text);
                MostrarListaSimple();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");
            }
            else
              if (rbListasDobles.Checked == true)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                listaDoble.EliminarDespues(txtReferenciaEliminar.Text);
                MostrarListaDoble();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");
            }
        }
        #endregion
        #region Búsqueda en la lista
        private void BuscarEmpleado(string nombre)
        {
            bool encontrado = false;
            dgEmpleados.Rows.Clear();


            if (rbStacks.Checked)
            {
                for (int i = 0; i < pilas.topePila; i++)
                {
                    if (pilas.employees[i].nombreE.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {

                        dgEmpleados.Rows.Add(
                            pilas.employees[i].nombreE,
                            pilas.employees[i].apellidoE,
                            pilas.employees[i].telefonoE,
                            pilas.employees[i].direccionE,
                            pilas.employees[i].edadE
                        );
                        encontrado = true;
                    }
                }
            }

            else if (rbSimpleColas.Checked)
            {
                for (int i = 0; i <= simples.final; i++)
                {
                    if (simples.employees[i].nombreE.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {

                        dgEmpleados.Rows.Add(
                            simples.employees[i].nombreE,
                            simples.employees[i].apellidoE,
                            simples.employees[i].telefonoE,
                            simples.employees[i].direccionE,
                            simples.employees[i].edadE
                        );
                        encontrado = true;
                    }
                }
            }

            else if (rbCircularesColas.Checked)
            {
                int i = colascirculares.frente;
                do
                {
                    var empleado = colascirculares.employees[i];

                    if (empleado != null && empleado.Value.nombreE.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {

                        dgEmpleados.Rows.Add(
                            empleado.Value.nombreE,
                            empleado.Value.apellidoE,
                            empleado.Value.telefonoE,
                            empleado.Value.direccionE,
                            empleado.Value.edadE
                        );
                        encontrado = true;
                    }

                    i = (i + 1) % colascirculares.Tamaño;
                } while (i != (colascirculares.fin + 1) % colascirculares.Tamaño);
            }
            else if (rbListasSimples.Checked)
            {
                Nodo resultado = ListaSimple.Busqueda_Desordenada(txtBusqueda.Text);
                if (resultado != null)
                {
                    bool existe = false;
                    foreach (DataGridViewRow row in dgLista.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(resultado.Empleado.nombreE, StringComparison.OrdinalIgnoreCase))
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe)
                    {
                        dgLista.Rows.Clear();
                        dgLista.Rows.Add(resultado.Empleado.nombreE, resultado.Empleado.apellidoE, resultado.Empleado.telefonoE, resultado.Empleado.direccionE, resultado.Empleado.edadE);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el elemento: " + txtBusqueda.Text);
                }
                txtBusqueda.Text = string.Empty;
            }
            else if (rbListasDobles.Checked)
            {
                NodoDoble resultado = listaDoble.Busqueda_Desordenada(txtBusqueda.Text);
                if (resultado != null)
                {
                    bool existe = false;
                    foreach (DataGridViewRow row in dgLista.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(resultado.Empleado.nombreE, StringComparison.OrdinalIgnoreCase))
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe)
                    {
                        dgLista.Rows.Clear();
                        dgLista.Rows.Add(resultado.Empleado.nombreE, resultado.Empleado.apellidoE, resultado.Empleado.telefonoE, resultado.Empleado.direccionE, resultado.Empleado.edadE);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el elemento: " + txtBusqueda.Text);
                }
                txtBusqueda.Text = string.Empty;
            }
            if (!encontrado)
            {
                MessageBox.Show("Empleado no encontrado.");
            }
        }



        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                MessageBox.Show("Por favor, ingresa un nombre para buscar.");
                return;
            }

            string nombre = txtBusqueda.Text.Trim();
            BuscarEmpleado(nombre);

        }
        #endregion
        private void RegistroOFC_Load(object sender, EventArgs e)
        {

        }
        #region  Ordenamientos

        public void HeapSort(Employee[] empleados, bool ascendente)
        {
            int n = empleados.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(empleados, n, i, ascendente);
            }
            for (int i = n - 1; i > 0; i--)
            {
                Employee temp = empleados[0];
                empleados[0] = empleados[i];
                empleados[i] = temp;
                Heapify(empleados, i, 0, ascendente);
            }
        }
        private void Heapify(Employee[] empleados, int n, int i, bool ascendente)
        {
            int extremum = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (ascendente)
            {

                if (left < n && empleados[left].edadE > empleados[extremum].edadE)
                {
                    extremum = left;
                }
                if (right < n && empleados[right].edadE > empleados[extremum].edadE)
                {
                    extremum = right;
                }
            }
            else
            {
                if (left < n && empleados[left].edadE < empleados[extremum].edadE)
                {
                    extremum = left;
                }
                if (right < n && empleados[right].edadE < empleados[extremum].edadE)
                {
                    extremum = right;
                }
            }

            if (extremum != i)
            {
                Employee swap = empleados[i];
                empleados[i] = empleados[extremum];
                empleados[extremum] = swap;
                Heapify(empleados, n, extremum, ascendente);
            }
        }
        private void btnHeap_Asc_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                HeapSort(empleadosArray, true);

                MostrarEmpleadosEnGrid();
            }
        }
        private void btnHeap_Desc_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                HeapSort(empleadosArray, false);

                MostrarEmpleadosEnGrid();
            }
        }
        private void ObtenerEmpleadosArray()
        {

            if (rbStacks.Checked == true)
            {
                empleadosArray = pilas.employees;
            }
            else if (rbSimpleColas.Checked == true)
            {
                empleadosArray = simples.employees;
            }
            else if (rbCircularesColas.Checked == true)
            {
                empleadosArray = colascirculares.employees.Where(e => e != null).Cast<Employee>().ToArray();
            }
            else if (rbListasSimples.Checked == true)
            {
                List<Employee> empleadosList = new List<Employee>();
                Nodo actual = ListaSimple.Primero;
                while (actual != null)
                {
                    empleadosList.Add(actual.Empleado);
                    actual = actual.Siguiente;
                }
                empleadosArray = empleadosList.ToArray();
            }
            else if (rbListasDobles.Checked == true)
            {
                List<Employee> empleadosList = new List<Employee>();
                NodoDoble actual = listaDoble.ObtenerCabeza();
                while (actual != null)
                {
                    empleadosList.Add(actual.Empleado);
                    actual = actual.Siguiente;
                }
                empleadosArray = empleadosList.ToArray();
            }
        }

        private void MostrarEmpleadosEnGrid()
        {
            if (rbStacks.Checked == true || rbCircularesColas.Checked == true || rbSimpleColas.Checked == true)
            {
                dgEmpleados.Rows.Clear();
                foreach (var empleado in empleadosArray)
                {
                    if (empleado.nombreE != null)
                    {
                        dgEmpleados.Rows.Add(empleado.nombreE, empleado.apellidoE, empleado.telefonoE, empleado.direccionE, empleado.edadE);
                    }
                }
            }
            else
                if (rbListasSimples.Checked == true || rbListasDobles.Checked == true)
            {
                dgLista.Rows.Clear();
                foreach (var empleado in empleadosArray)
                {
                    if (empleado.nombreE != null)
                    {
                        dgLista.Rows.Add(empleado.nombreE, empleado.apellidoE, empleado.telefonoE, empleado.direccionE, empleado.edadE);
                    }
                }
            }

        }
        Employee[] empleadosArray = null;
        public void SelectionSortAscendente(Employee[] empleados)
        {
            int n = empleados.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (empleados[j].edadE < empleados[min].edadE)
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    Employee temp = empleados[i];
                    empleados[i] = empleados[min];
                    empleados[min] = temp;
                }
            }
        }
        public void SelectionSortDescendente(Employee[] empleados)
        {
            int n = empleados.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (empleados[j].edadE > empleados[max].edadE)
                    {
                        max = j;
                    }
                }

                if (max != i)
                {
                    Employee temp = empleados[i];
                    empleados[i] = empleados[max];
                    empleados[max] = temp;
                }
            }
        }

        public void QuickSort(Employee[] empleados, bool ascendente)
        {
            QuickSortRecursive(empleados, 0, empleados.Length - 1, ascendente);
        }

        private void QuickSortRecursive(Employee[] empleados, int low, int high, bool ascendente)
        {
            if (low < high)
            {
                int pivotIndex = Partition(empleados, low, high, ascendente);
                QuickSortRecursive(empleados, low, pivotIndex - 1, ascendente);
                QuickSortRecursive(empleados, pivotIndex + 1, high, ascendente);
            }
        }

        private int Partition(Employee[] empleados, int low, int high, bool ascendente)
        {
            Employee pivot = empleados[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                bool condicion = ascendente ? empleados[j].edadE < pivot.edadE : empleados[j].edadE > pivot.edadE;
                if (condicion)
                {
                    i++;
                    Swap(empleados, i, j);
                }
            }
            Swap(empleados, i + 1, high);
            return i + 1;
        }


        public void ShakeSort(Employee[] empleados, bool ascendente)
        {
            int left = 0;
            int right = empleados.Length - 1;
            bool swapped = true;

            while (left < right && swapped)
            {
                swapped = false;

                for (int i = left; i < right; i++)
                {
                    bool condicion = ascendente ? empleados[i].edadE > empleados[i + 1].edadE : empleados[i].edadE < empleados[i + 1].edadE;
                    if (condicion)
                    {
                        Swap(empleados, i, i + 1);
                        swapped = true;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    bool condicion = ascendente ? empleados[i].edadE < empleados[i - 1].edadE : empleados[i].edadE > empleados[i - 1].edadE;
                    if (condicion)
                    {
                        Swap(empleados, i, i - 1);
                        swapped = true;
                    }
                }
                left++;
            }
        }


        private void Swap(Employee[] empleados, int i, int j)
        {
            Employee temp = empleados[i];
            empleados[i] = empleados[j];
            empleados[j] = temp;
        }



        private void button7_Click(object sender, EventArgs e)
        {

            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                SelectionSortAscendente(empleadosArray);

                MostrarEmpleadosEnGrid();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                SelectionSortDescendente(empleadosArray);

                MostrarEmpleadosEnGrid();
            }

        }

       

        private void btnLimpiarSeleccion_Click(object sender, EventArgs e)
        {
            rbListasDobles.Enabled = true;
            rbListasSimples.Enabled = true;
            dgEmpleados.Rows.Clear();
        }

        private void btnHeapAscLista_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                HeapSort(empleadosArray, true);

                MostrarEmpleadosEnGrid();
            }

        }

        private void btnDescHeapLista_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                HeapSort(empleadosArray, false);

                MostrarEmpleadosEnGrid();
            }
        }

        private void btnSelectionAscLista_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                SelectionSortAscendente(empleadosArray);

                MostrarEmpleadosEnGrid();
            }
        }
        private void OrdenarEdadesBuble(bool ascendente)
        {
            if (rbStacks.Checked || rbSimpleColas.Checked || rbCircularesColas.Checked)
            {
                int rowCount = dgEmpleados.Rows.Count;
                int[] edades = new int[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    edades[i] = Convert.ToInt32(dgEmpleados.Rows[i].Cells[4].Value);
                }


                for (int i = 0; i < edades.Length - 1; i++)
                {
                    for (int j = 0; j < edades.Length - i - 1; j++)
                    {
                        bool condicion = ascendente ? edades[j] > edades[j + 1] : edades[j] < edades[j + 1];
                        if (condicion)
                        {

                            int temp = edades[j];
                            edades[j] = edades[j + 1];
                            edades[j + 1] = temp;
                        }
                    }
                }


                for (int i = 0; i < rowCount; i++)
                {
                    dgEmpleados.Rows[i].Cells[4].Value = edades[i];
                }
            }
            else if (rbListasSimples.Checked || rbListasDobles.Checked)
            {
                int rowCount = dgLista.Rows.Count;
                int[] edades = new int[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    edades[i] = Convert.ToInt32(dgLista.Rows[i].Cells[4].Value);
                }


                for (int i = 0; i < edades.Length - 1; i++)
                {
                    for (int j = 0; j < edades.Length - i - 1; j++)
                    {
                        bool condicion = ascendente ? edades[j] > edades[j + 1] : edades[j] < edades[j + 1];
                        if (condicion)
                        {

                            int temp = edades[j];
                            edades[j] = edades[j + 1];
                            edades[j + 1] = temp;
                        }
                    }
                }


                for (int i = 0; i < rowCount; i++)
                {
                    dgLista.Rows[i].Cells[4].Value = edades[i];
                }
            }

        }

        private void btnDescSelectionLista_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();

            if (empleadosArray != null)
            {
                SelectionSortAscendente(empleadosArray);

                MostrarEmpleadosEnGrid();
            }
        }

        private void btnBuscarListas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Por favor, ingresa un nombre para buscar.");
                return;
            }

            string nombre = txtBuscar.Text.Trim();
            BuscarEmpleado(nombre);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OrdenarEdadesBuble(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OrdenarEdadesBuble(false);
        }

        private void btm_Click(object sender, EventArgs e)
        {
            OrdenarEdadesBuble(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrdenarEdadesBuble(false);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                QuickSort(empleadosArray, true);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                QuickSort(empleadosArray, false);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                QuickSort(empleadosArray, true);
                MostrarEmpleadosEnGrid();
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                QuickSort(empleadosArray, false);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                ShakeSort(empleadosArray, true);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                ShakeSort(empleadosArray, false);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                ShakeSort(empleadosArray, true);
                MostrarEmpleadosEnGrid();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ObtenerEmpleadosArray();
            if (empleadosArray != null)
            {
                ShakeSort(empleadosArray, false);
                MostrarEmpleadosEnGrid();
            }
        }
        #endregion
    }

}
