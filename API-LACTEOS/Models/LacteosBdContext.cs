﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_LACTEOS.Models;

public partial class LacteosBdContext : DbContext
{
    public LacteosBdContext()
    {
    }

    public LacteosBdContext(DbContextOptions<LacteosBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesVentum> DetallesVenta { get; set; }

    public virtual DbSet<Devolucione> Devoluciones { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Perdida> Perdidas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosProveedore> ProductosProveedores { get; set; }

    public virtual DbSet<Progreso> Progresos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<UnidadesMedida> UnidadesMedidas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<ViewFactura> ViewFacturas { get; set; }

    public virtual DbSet<ViewUsuario> ViewUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3213E83FEA89A469");

            entity.HasIndex(e => e.Ruc, "UQ__Clientes__C2B74E61AE9DC574").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoCliente");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.Ruc)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compras__3213E83F1AE91C6E");

            entity.HasIndex(e => e.NumeroFacutura, "UQ__Compras__B387EDB9AF399323").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("datetime")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NumeroFacutura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroFacutura");
            entity.Property(e => e.TotalCompra)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("totalCompra");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras__idProve__48CFD27E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras__idUsuar__49C3F6B7");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3213E83F369DE147");

            entity.ToTable("DetallesCompra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadComprada).HasColumnName("cantidadComprada");
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precioUnitario");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__DetallesC__idCom__4E88ABD4");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesC__idPro__4F7CD00D");
        });

        modelBuilder.Entity<DetallesVentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3213E83F1521C407");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadVendida).HasColumnName("cantidadVendida");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precioUnitario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesV__idPro__5BE2A6F2");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetallesV__idVen__5AEE82B9");
        });

        modelBuilder.Entity<Devolucione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devoluci__3213E83FEFF72305");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadDevuelta).HasColumnName("cantidadDevuelta");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.RazonDevolucion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonDevolucion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__idPro__6477ECF3");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Devolucio__idVen__6383C8BA");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estado__3213E83FB95ABDA9");

            entity.ToTable("Estado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreEstado");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3213E83F2D764D75");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Plazo).HasColumnName("plazo");
            entity.Property(e => e.TotalPago)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("totalPago");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__idVenta__5FB337D6");
        });

        modelBuilder.Entity<Perdida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Perdidas__3213E83F92582D86");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadPerdida).HasColumnName("cantidadPerdida");
            entity.Property(e => e.FechaPerdida)
                .HasColumnType("datetime")
                .HasColumnName("fechaPerdida");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.RazonPerdida)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonPerdida");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Perdida)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Perdidas__idProd__68487DD7");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83F65165FC9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadProducto).HasColumnName("cantidadProducto");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcionProducto");
            entity.Property(e => e.FechaExpiracionProducto)
                .HasColumnType("datetime")
                .HasColumnName("fechaExpiracionProducto");
            entity.Property(e => e.IdTipoProducto).HasColumnName("idTipoProducto");
            entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");
            entity.Property(e => e.MinimoStockProducto).HasColumnName("minimoStockProducto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.PrecioProducto)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precioProducto");

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idTip__3F466844");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idUni__403A8C7D");
        });

        modelBuilder.Entity<ProductosProveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83F897C9883");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Productos__idPro__440B1D61");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Productos__idPro__4316F928");
        });

        modelBuilder.Entity<Progreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Progreso__3213E83F87C5CACB");

            entity.ToTable("Progreso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreProgreso)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreProgreso");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3213E83F0D5CF67F");

            entity.HasIndex(e => e.RucProveedor, "UQ__Proveedo__0CBBF3D0C0C165E8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdEstado)
                .HasDefaultValue(1)
                .HasColumnName("idEstado");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.RucProveedor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rucProveedor");
            entity.Property(e => e.TelefonoProveedor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefonoProveedor");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proveedor__idEst__3A81B327");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F9FFCBA08");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoProd__3213E83F1EBAAC52");

            entity.ToTable("TipoProducto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreTipoProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreTipoProducto");
        });

        modelBuilder.Entity<UnidadesMedida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unidades__3213E83F16CB73F2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DescripcionUnidadMedida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descripcionUnidadMedida");
            entity.Property(e => e.NombreUnidadMedida)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("nombreUnidadMedida");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F20FBF5A3");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__2A586E0B5F90A329").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__A0436BD7D6AD2916").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contra");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.IdEstado)
                .HasDefaultValue(1)
                .HasColumnName("idEstado");
            entity.Property(e => e.IdRole)
                .HasDefaultValue(2)
                .HasColumnName("idRole");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__idEsta__32E0915F");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__idRole__31EC6D26");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ventas__3213E83F193EF567");

            entity.HasIndex(e => e.NumeroFactura, "UQ__Ventas__16B1D021D08739FE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroFactura");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipoVenta");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("totalVenta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__idClient__5535A963");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__idUsuari__5629CD9C");
        });

        modelBuilder.Entity<ViewFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewFactura");

            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoCliente");
            entity.Property(e => e.CantidadVendida).HasColumnName("cantidadVendida");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroFactura");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipoVenta");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("totalVenta");
        });

        modelBuilder.Entity<ViewUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewUsuarios");

            entity.Property(e => e.Contra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contra");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreEstado");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
