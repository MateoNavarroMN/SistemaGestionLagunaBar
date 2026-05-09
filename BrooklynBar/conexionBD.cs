using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Linq;

namespace BrooklynBar
{
    internal class conexionBD
    {
        private string cadena;

        public conexionBD()
        {
            cadena = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./BrooklynBar.mdb;";
        }

        #region Metodos Reutilizables
        public DataTable LeerTabla(string consulta, List<OleDbParameter> parametros)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        if (parametros != null && parametros.Count > 0)
                        {
                            comando.Parameters.AddRange(parametros.ToArray());
                        }

                        using (OleDbDataAdapter adaptador = new OleDbDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar consulta: " + ex.Message);
            }
            return tabla;
        }
        //
        public DataSet LeerTabla2(string consulta, List<OleDbParameter> parametros)
        {
            DataSet tabla = new DataSet();
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros.ToArray());
                        }

                        using (OleDbDataAdapter adaptador = new OleDbDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar consulta: " + ex.Message);
            }
            return tabla;
        }

        private bool EjecutarConsulta(string consulta, List<OleDbParameter> parametros)
        {
            bool resultado = false;
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros.ToArray());
                        }
                        conexion.Open();
                        resultado = comando.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar consulta: " + ex.Message);
            }
            return resultado;
        }

        public bool ExisteRegistro(string tabla, string columna, object valor)
        {
            bool resultado = false;
            string consulta = $"SELECT COUNT(*) FROM {tabla} WHERE {columna}=?";
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new OleDbParameter("?", valor));
                        conexion.Open();
                        resultado = Convert.ToInt32(comando.ExecuteScalar()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar existencia en {tabla}: " + ex.Message);
            }
            return resultado;
        }
        #endregion

        #region Metodos Categorias Menus
        public DataTable ObtenerCategoriasMenus()
        {
            return LeerTabla("SELECT * FROM CategoriasMenus", null);
        }

        public bool AgregarCategoriaMenu(clsCategoriaMenu categoria)
        {
            string consulta = "INSERT INTO CategoriasMenus (Nombre) VALUES (?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = categoria.Nombre },
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarCategoriaMenu(clsCategoriaMenu categoria)
        {
            string consulta = "UPDATE CategoriasMenus SET Nombre=? WHERE ID_Categoria=?";
            if (!ExisteRegistro("CategoriasMenus", "ID_Categoria", categoria.ID_Categoria))
            {
                MessageBox.Show("La categoria que intentas modificar no existe.");
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = categoria.Nombre },
                new OleDbParameter("@ID_Categoria", OleDbType.Integer) { Value = categoria.ID_Categoria },
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarCategoriaMenu(int idCategoria)
        {
            if (!ExisteRegistro("CategoriasMenus", "ID_Categoria", idCategoria))
            {
                MessageBox.Show("La categoria que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM CategoriasMenus WHERE ID_Categoria=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Categoria", OleDbType.Integer) { Value = idCategoria }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Menus
        public DataTable ObtenerMenus()
        {
            return LeerTabla("SELECT * FROM Menus", null);
        }

        //no implementada
        public DataSet ObtenerMenus2()
        {
            return LeerTabla2("SELECT M.ID_Menu, C.Nombre AS Categoria, M.Nombre,  M.Precio, M.Descripcion " +
                              "FROM Menus AS M INNER JOIN CategoriasMenus AS C ON M.ID_Categoria=C.ID_Categoria", null);
        }

        public DataTable ObtenerMenu(int menu)
        {
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter ("?", menu)
            };
            return LeerTabla("SELECT * FROM Menus WHERE ID_Menu=?", parametros);
        }

        public bool AgregarMenu(clsMenu menu)
        {
            string consulta = "INSERT INTO Menus (ID_Categoria, Nombre, Precio, Descripcion) VALUES (?, ?, ?, ?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Categoria", OleDbType.Integer) { Value = menu.ID_Categoria },
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = menu.Nombre },
                new OleDbParameter("@Precio", OleDbType.Currency) { Value = menu.Precio },
                new OleDbParameter("@Descripcion", OleDbType.LongVarChar) { Value = menu.Descripcion }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarMenu(clsMenu menu)
        {
            string consulta = "UPDATE Menus SET ID_Categoria=?, Nombre=?, Precio=?, Descripcion=? WHERE ID_Menu=?";
            if (!ExisteRegistro("Menus", "ID_Menu", menu.ID_Menu))
            {
                MessageBox.Show("El menú que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Categoria", OleDbType.Integer) { Value = menu.ID_Categoria },
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = menu.Nombre },
                new OleDbParameter("@Precio", OleDbType.Currency) { Value = menu.Precio },
                new OleDbParameter("@Descripcion", OleDbType.LongVarChar) { Value = menu.Descripcion },
                new OleDbParameter("@ID_Menu", OleDbType.Integer) { Value = menu.ID_Menu }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarMenu(int idMenu)
        {
            if (!ExisteRegistro("Menus", "ID_Menu", idMenu))
            {
                MessageBox.Show("El menú que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Menus WHERE ID_Menu=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Menu", OleDbType.Integer) { Value = idMenu }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Detalles Pedido
        public DataSet ObtenerDetallesPedido()
        {
            return LeerTabla2("SELECT D.ID_Detalle, D.ID_Pedido, M.Nombre, D.Cantidad, D.Precio_Unitario " +
                              "FROM DetallesPedido AS D INNER JOIN Menus AS M ON D.ID_Menu=M.ID_Menu", null);
        }

        public DataTable ObtenerDetallePedido(int idPedido)
        {
            string consulta = "SELECT D.ID_Detalle, D.ID_Pedido, M.Nombre, D.Cantidad, D.Precio_Unitario " +
                              "FROM DetallesPedido AS D INNER JOIN Menus AS M ON D.ID_Menu=M.ID_Menu " +
                              "WHERE ID_Pedido = ?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = idPedido }
            };

            return LeerTabla(consulta, parametros);
        }

        public bool AgregarDetallePedido(clsDetallePedido detalle)
        {
            string consulta = "INSERT INTO DetallesPedido (ID_Menu, ID_Pedido, Cantidad, Precio_Unitario) VALUES (?, ?, ?, ?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Menu", OleDbType.Integer) { Value = detalle.ID_Menu },
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = detalle.ID_Pedido },
                new OleDbParameter("@Cantidad", OleDbType.Integer) { Value = detalle.Cantidad },
                new OleDbParameter("@Precio_Unitario", OleDbType.Currency) { Value = detalle.Precio_Unitario }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarDetallePedido(clsDetallePedido detalle)
        {
            string consulta = "UPDATE DetallesPedido SET ID_Menu=?, ID_Pedido=?, Cantidad=?, Precio_Unitario=? WHERE ID_Detalle=?";
            if (!ExisteRegistro("DetallesPedido", "ID_Detalle", detalle.ID_Detalle))
            {
                MessageBox.Show("El detalle del pedido que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Menu", OleDbType.Integer) { Value = detalle.ID_Menu },
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = detalle.ID_Pedido },
                new OleDbParameter("@Cantidad", OleDbType.Integer) { Value = detalle.Cantidad },
                new OleDbParameter("@Precio_Unitario", OleDbType.Currency) { Value = detalle.Precio_Unitario },
                new OleDbParameter("@ID_Detalle", OleDbType.Integer) { Value = detalle.ID_Detalle }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarDetallePedido(int idDetalle)
        {
            if (!ExisteRegistro("DetallesPedido", "ID_Detalle", idDetalle))
            {
                MessageBox.Show("El detalle del pedido que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM DetallesPedido WHERE ID_Detalle=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Detalle", OleDbType.Integer) { Value = idDetalle }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarDetallePedido(int idpedido, int idmenu)
        {
            string consulta = "DELETE FROM DetallesPedido WHERE ID_Pedido=? AND ID_Menu=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = idpedido },
                new OleDbParameter("@ID_Menu", OleDbType.Integer) { Value = idmenu }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Pedidos
        public DataTable ObtenerPedidos()
        {
            return LeerTabla("SELECT * FROM Pedidos", null);
        }

        public DataTable ObtenerPedidoMesa(int idMesa)
        {
            string consulta = "SELECT ID_Pedido, Estado FROM Pedidos WHERE ID_Mesa = ? AND Estado = 'Abierto'";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = idMesa }
            };

            return LeerTabla(consulta, parametros);
        }

        public DataTable ObtenerPedido(int idPedido)
        {
            string consulta = "SELECT ID_Mesa, Fecha, DNI_Empleado, Estado FROM Pedidos WHERE ID_Pedido = ? AND Estado = 'Abierto'";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = idPedido }
            };

            return LeerTabla(consulta, parametros);
        }

        public bool AgregarPedido(clsPedido pedido)
        {
            string consulta = "INSERT INTO Pedidos (ID_Mesa, Fecha, Estado, DNI_Empleado) VALUES (?, ?, ? ,?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = pedido.ID_Mesa },
                new OleDbParameter("@Fecha", OleDbType.DBTimeStamp) { Value = pedido.Fecha },
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = pedido.Estado },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = pedido.DNI_Empleado }
            };
            OcuparMesa(pedido.ID_Mesa, pedido.DNI_Empleado);
            return EjecutarConsulta(consulta, parametros);
        }

        public int AgregarPedidoRetornarID(clsPedido pedido)
        {
            int id = 0;
            string consulta = "INSERT INTO Pedidos (ID_Mesa, Fecha, Estado, DNI_Empleado) VALUES (?, ?, ? ,?)";

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = pedido.ID_Mesa },
                new OleDbParameter("@Fecha", OleDbType.DBTimeStamp) { Value = pedido.Fecha },
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = pedido.Estado },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = pedido.DNI_Empleado ?? (object)DBNull.Value}
            };

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadena))
                {
                    using (OleDbCommand comando = new OleDbCommand(consulta, conexion))
                    {
                        comando.Parameters.AddRange(parametros.ToArray());
                        conexion.Open();
                        comando.ExecuteNonQuery();

                        comando.CommandText = "SELECT @@IDENTITY";
                        comando.Parameters.Clear();
                        var result = comando.ExecuteScalar();
                        id = Convert.ToInt32(result);
                        OcuparMesa(pedido.ID_Mesa, pedido.DNI_Empleado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar consulta: " + ex.Message);
            }

            return id;
        }

        public bool EditarPedido(clsPedido pedido)
        {
            string consulta = "UPDATE Pedidos SET ID_Mesa=?, Fecha=?, Estado=?, DNI_Empleado=? WHERE ID_Pedido=?";
            if (!ExisteRegistro("Pedidos", "ID_Pedido", pedido.ID_Pedido))
            {
                MessageBox.Show("El pedido que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = pedido.ID_Mesa },
                new OleDbParameter("@Fecha", OleDbType.DBTimeStamp) { Value = pedido.Fecha },
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = pedido.Estado },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = pedido.DNI_Empleado },
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = pedido.ID_Pedido }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarPedido(int idPedido)
        {
            if (!ExisteRegistro("Pedidos", "ID_Pedido", idPedido))
            {
                MessageBox.Show("El pedido que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Pedidos WHERE ID_Pedido=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = idPedido }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool CerrarPedido(int idPedido)
        {
            string consulta = "UPDATE Pedidos SET Estado = 'Cerrado' WHERE ID_Pedido = ?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = idPedido }
            };

            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Empleado
        public DataTable ObtenerEmpleados()
        {
            return LeerTabla("SELECT * FROM Empleados", null);
        }

        public DataTable ObtenerEmpleados(int nivel)
        {
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter ("?", nivel)
            };
            return LeerTabla("SELECT * FROM Empleados WHERE ID_Nivel=?", parametros);
        }

        public bool AgregarEmpleado(clsEmpleado empleado)
        {
            string consulta = "INSERT INTO Empleados (DNI_Empleado, Nombre_Apellido, Sexo, Telefono, Direccion, ID_Barrio, Contacto_Emergencia, ID_Nivel, Contrasena) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = empleado.DNI_Empleado },
                new OleDbParameter("@Nombre_Apellido", OleDbType.VarChar) { Value = empleado.Nombre_Apellido },
                new OleDbParameter("@Sexo", OleDbType.VarChar) { Value = empleado.Sexo },
                new OleDbParameter("@Telefono", OleDbType.VarChar) { Value = empleado.Telefono },
                new OleDbParameter("@Direccion", OleDbType.VarChar) { Value = empleado.Direccion },
                new OleDbParameter("@ID_Barrio", OleDbType.Integer) { Value = empleado.ID_Barrio },
                new OleDbParameter("@Contacto_Emergencia", OleDbType.VarChar) { Value = empleado.Contacto_Emergencia },
                new OleDbParameter("@ID_Nivel", OleDbType.Integer) { Value = empleado.ID_Nivel },
                new OleDbParameter("@Contrasena", OleDbType.VarChar) { Value = empleado.Contrasena }
            };

            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarEmpleado(clsEmpleado empleado)
        {
            string consulta = "UPDATE Empleados SET Nombre_Apellido=?, Sexo=?, Telefono=?, Direccion=?, ID_Barrio=?, Contacto_Emergencia=?, ID_Nivel=?, Contrasena=? WHERE DNI_Empleado=?";
            if (!ExisteRegistro("Empleados", "DNI_Empleado", empleado.DNI_Empleado))
            {
                MessageBox.Show("El Empleado que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre_Apellido", OleDbType.VarChar) { Value = empleado.Nombre_Apellido },
                new OleDbParameter("@Sexo", OleDbType.VarChar) { Value = empleado.Sexo },
                new OleDbParameter("@Telefono", OleDbType.VarChar) { Value = empleado.Telefono },
                new OleDbParameter("@Direccion", OleDbType.VarChar) { Value = empleado.Direccion },
                new OleDbParameter("@ID_Barrio", OleDbType.Integer) { Value = empleado.ID_Barrio },
                new OleDbParameter("@Contacto_Emergencia", OleDbType.VarChar) { Value = empleado.Contacto_Emergencia },
                new OleDbParameter("@ID_Nivel", OleDbType.Integer) { Value = empleado.ID_Nivel },
                new OleDbParameter("@Contrasena", OleDbType.VarChar) { Value = empleado.Contrasena },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = empleado.DNI_Empleado }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarEmpleado(int dniEmpleado)
        {
            if (!ExisteRegistro("Empleados", "DNI_Empleado", dniEmpleado))
            {
                MessageBox.Show("El empleado que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Empleados WHERE DNI_Empleado=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = dniEmpleado }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Mesas
        public DataTable ObtenerMesas()
        {
            return LeerTabla("SELECT * FROM Mesas", null);
        }

        public DataSet ObtenerMesas2()
        {
            return LeerTabla2("SELECT M.ID_Mesa as Mesa, IIF(E.Nombre_Apellido IS NULL, '---', E.Nombre_Apellido) AS Empleado, M.Estado FROM Mesas AS M LEFT JOIN Empleados AS E ON M.DNI_Empleado=E.DNI_Empleado", null);
        }

        public DataTable ObtenerEstadoMesa(int idMesa)
        {
            string consulta = "SELECT Estado, DNI_Empleado FROM Mesas WHERE ID_Mesa = ?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = idMesa }
            };

            return LeerTabla(consulta, parametros);
        }

        public void OcuparMesa(int idMesa, string DNI)
        {
            string consulta = "UPDATE Mesas SET DNI_Empleado=?, Estado=? WHERE ID_Mesa=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = DNI },
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = "Ocupada" },
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = idMesa }
            };
            EjecutarConsulta(consulta, parametros);
        }

        public bool LiberarMesa(int idMesa)
        {
            string consulta = "UPDATE Mesas SET Estado = 'Disponible', DNI_Empleado = NULL WHERE ID_Mesa = ?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = idMesa }
            };

            return EjecutarConsulta(consulta, parametros);
        }

        public bool AgregarMesa(string estado)
        {
            string consulta = "INSERT INTO Mesas (Estado) VALUES (?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = estado }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarMesa(clsMesa mesa)
        {
            string consulta = "UPDATE Mesas SET DNI_Empleado=?, Estado=? WHERE ID_Mesa=?";
            if (!ExisteRegistro("Mesas", "ID_Mesa", mesa.ID_Mesa))
            {
                MessageBox.Show("La mesa que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = mesa.DNI_Empleado },
                new OleDbParameter("@Estado", OleDbType.VarChar) { Value = mesa.Estado },
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = mesa.ID_Mesa }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarMesa(int idMesa)
        {
            if (!ExisteRegistro("Mesas", "ID_Mesa", idMesa))
            {
                MessageBox.Show("La mesa que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Mesas WHERE ID_Mesa=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Mesa", OleDbType.Integer) { Value = idMesa }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Ventas
        public DataTable ObtenerVentas()
        {
            return LeerTabla("SELECT * FROM Ventas", null);
        }

        public DataTable ObtenerVentas2()
        {
            string consulta = "SELECT V.ID_Pedido AS Pedido, E.Nombre_Apellido AS Empleado, V.Fecha, V.Total FROM Ventas AS V INNER JOIN Empleados AS E ON V.DNI_Empleado=E.DNI_Empleado";
            return LeerTabla(consulta, null);
        }

        public bool AgregarVenta(clsVenta venta)
        {
            string consulta = "INSERT INTO Ventas (ID_Pedido, DNI_Empleado, Fecha, Total) VALUES (?, ?, ?, ?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = venta.ID_Pedido },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = venta.DNI_Empleado },
                new OleDbParameter("@Fecha", OleDbType.DBTimeStamp) { Value = venta.Fecha },
                new OleDbParameter("@Total", OleDbType.Currency) { Value = venta.Total }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarVenta(clsVenta venta)
        {
            string consulta = "UPDATE Ventas SET ID_Pedido=?, DNI_Empleado=?, Fecha=?, Total=? WHERE ID_Venta=?";
            if (!ExisteRegistro("Ventas", "ID_Venta", venta.ID_Venta))
            {
                MessageBox.Show("La venta que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Pedido", OleDbType.Integer) { Value = venta.ID_Pedido },
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = venta.DNI_Empleado },
                new OleDbParameter("@Fecha", OleDbType.DBTimeStamp) { Value = venta.Fecha },
                new OleDbParameter("@Total", OleDbType.Currency) { Value = venta.Total },
                new OleDbParameter("@ID_Venta", OleDbType.Integer) { Value = venta.ID_Venta }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarVenta(int idVenta)
        {
            if (!ExisteRegistro("Ventas", "ID_Venta", idVenta))
            {
                MessageBox.Show("La venta que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Ventas WHERE ID_Venta=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Venta", OleDbType.Integer) { Value = idVenta }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Niveles
        public DataTable ObtenerNiveles()
        {
            return LeerTabla("SELECT * FROM Niveles", null);
        }

        public bool AgregarNivel(clsNivel nivel)
        {
            string consulta = "INSERT INTO Niveles (Nombre) VALUES (?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = nivel.Nombre }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarNivel(clsNivel nivel)
        {
            string consulta = "UPDATE Niveles SET Nombre=? WHERE ID_Nivel=?";
            if (!ExisteRegistro("Niveles", "ID_Nivel", nivel.ID_Nivel))
            {
                MessageBox.Show("El nivel que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = nivel.Nombre },
                new OleDbParameter("@ID_Nivel", OleDbType.Integer) { Value = nivel.ID_Nivel }

            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarNivel(int idNivel)
        {
            if (!ExisteRegistro("Niveles", "ID_Nivel", idNivel))
            {
                MessageBox.Show("El nivel que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Niveles WHERE ID_Nivel=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Nivel", OleDbType.Integer) { Value = idNivel }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Metodos Barrios
        public DataTable ObtenerBarrios()
        {
            return LeerTabla("SELECT * FROM Barrios", null);
        }

        public bool AgregarBarrio(clsBarrio barrio)
        {
            string consulta = "INSERT INTO Barrios (Nombre) VALUES (?)";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = barrio.Nombre }
            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EditarBarrio(clsBarrio barrio)
        {
            string consulta = "UPDATE Barrios SET Nombre=? WHERE ID_Barrio=?";
            if (!ExisteRegistro("Barrios", "ID_Barrio", barrio.ID_Barrio))
            {
                MessageBox.Show("El barrio que intentas modificar no existe.");
                return false;
            }

            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@Nombre", OleDbType.VarChar) { Value = barrio.Nombre },
                new OleDbParameter("@ID_Barrio", OleDbType.Integer) { Value = barrio.ID_Barrio }

            };
            return EjecutarConsulta(consulta, parametros);
        }

        public bool EliminarBarrio(int idBarrio)
        {
            if (!ExisteRegistro("Barrios", "ID_Barrio", idBarrio))
            {
                MessageBox.Show("El barrio que intentas modificar no existe.");
                return false;
            }

            string consulta = "DELETE FROM Barrios WHERE ID_Barrio=?";
            List<OleDbParameter> parametros = new List<OleDbParameter>
            {
                new OleDbParameter("@ID_Barrio", OleDbType.Integer) { Value = idBarrio }
            };
            return EjecutarConsulta(consulta, parametros);
        }
        #endregion

        #region Graficos Estadisticas
        public void GraficarVenta(Chart grafico)
        {
            string consulta = "SELECT Fecha, SUM(Total) AS Total FROM Ventas GROUP BY Fecha ORDER BY Fecha";
            DataTable tabla = LeerTabla(consulta, null);
            grafico.Series.Clear();

            Series serie = new Series("Ventas Por Fecha");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;
            serie.Color = Color.SteelBlue;

            foreach (DataRow fila in tabla.Rows)
            {
                DateTime fecha = Convert.ToDateTime(fila["Fecha"]);
                decimal total = Convert.ToDecimal(fila["Total"]);

                serie.Points.AddXY(fecha.ToShortDateString(), total);
            }
            grafico.Series.Add(serie);
            grafico.ChartAreas[0].AxisX.Interval = 1;
            grafico.ChartAreas[0].AxisX.Title = "Fecha";
            grafico.ChartAreas[0].AxisY.Title = "Importe Total";
            var area = grafico.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false;
        }

        public void GraficarTopMenus(Chart grafico)
        {
            string consulta = "SELECT M.Nombre AS Menu, SUM(D.Cantidad) AS Cantidad FROM Menus AS M INNER JOIN DetallesPedido AS D ON M.ID_Menu=D.ID_Menu GROUP BY M.Nombre ORDER BY SUM(D.Cantidad) DESC";
            DataTable tabla = LeerTabla(consulta, null);
            grafico.Series.Clear();

            Series serie = new Series("Menus Vendidos");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;
            serie.Color = Color.SlateBlue;

            foreach (DataRow fila in tabla.Rows)
            {
                string menu = fila["Menu"].ToString();
                string cantidad = fila["Cantidad"].ToString();

                serie.Points.AddXY(menu, cantidad);
            }
            grafico.Series.Add(serie);
            grafico.ChartAreas[0].AxisX.Interval = 1;
            grafico.ChartAreas[0].AxisX.Title = "Menu";
            grafico.ChartAreas[0].AxisY.Title = "Cantidad";
            var area = grafico.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false;
        }

        public void GraficarVentasPorEmpleado(Chart grafico)
        {
            string consulta = "SELECT E.Nombre_Apellido, SUM(V.Total) AS Total FROM Ventas AS V INNER JOIN Empleados AS E ON V.DNI_Empleado=E.DNI_Empleado GROUP BY Nombre_Apellido";

            DataTable tabla = LeerTabla(consulta, null);
            grafico.Series.Clear();

            Series serie = new Series("Ventas por Empleado");
            serie.ChartType = SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;
            serie.Color = Color.Crimson;

            foreach (DataRow fila in tabla.Rows)
            {
                string nombre = fila["Nombre_Apellido"].ToString();
                decimal total = Convert.ToDecimal(fila["Total"]);
                serie.Points.AddXY(nombre, total);
            }

            grafico.Series.Add(serie);
            grafico.ChartAreas[0].AxisX.Interval = 1;
            grafico.ChartAreas[0].AxisX.Title = "Empleado";
            grafico.ChartAreas[0].AxisY.Title = "Total Recaudado";
            var area = grafico.ChartAreas[0];
            area.AxisX.MajorGrid.Enabled = false;
        }
        #endregion

        public DataSet ValidarLogin(string dni, string contraseña)
        {
            string consulta = "SELECT E.DNI_Empleado, E.Nombre_Apellido, N.Nombre AS Nivel FROM Empleados AS E INNER JOIN Niveles AS N ON E.ID_Nivel=N.ID_Nivel WHERE DNI_Empleado=? AND Contrasena=?";

            List<OleDbParameter> parametros = new List<OleDbParameter>()
            {
                new OleDbParameter("@DNI_Empleado", OleDbType.VarChar) { Value = dni},
                new OleDbParameter("@Contrasena", OleDbType.VarChar) { Value = contraseña }
            };

            return LeerTabla2(consulta, parametros);
        }
    }
}