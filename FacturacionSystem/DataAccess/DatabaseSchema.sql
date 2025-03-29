-- Tabla Clientes
CREATE TABLE IF NOT EXISTS Clientes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    RTN TEXT UNIQUE,
    Direccion TEXT,
    Telefono TEXT,
    Email TEXT,
    FechaRegistro TEXT DEFAULT CURRENT_TIMESTAMP
);

-- Tabla Productos
CREATE TABLE IF NOT EXISTS Productos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Descripcion TEXT,
    Precio REAL NOT NULL,
    IVA REAL DEFAULT 0.15,
    Activo INTEGER DEFAULT 1
);

-- Tabla Facturas
CREATE TABLE IF NOT EXISTS Facturas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    NumeroFactura TEXT UNIQUE NOT NULL,
    Fecha TEXT DEFAULT CURRENT_TIMESTAMP,
    ClienteId INTEGER NOT NULL,
    Subtotal REAL NOT NULL,
    Impuestos REAL NOT NULL,
    Total REAL NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla DetalleFactura
CREATE TABLE IF NOT EXISTS DetalleFactura (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FacturaId INTEGER NOT NULL,
    ProductoId INTEGER NOT NULL,
    Cantidad INTEGER NOT NULL,
    PrecioUnitario REAL NOT NULL,
    Total REAL NOT NULL,
    FOREIGN KEY (FacturaId) REFERENCES Facturas(Id),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);