using System;
using System.Security.Cryptography;
using System.Text;
using FacturacionSystem.Models;
using FacturacionSystem.DAL.Repositories;

namespace FacturacionSystem.BLL.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public Usuario Autenticar(string nombreUsuario, string password)
        {
            try
            {
                var usuario = _usuarioRepository.GetByNombre(nombreUsuario);
                
                if (usuario != null && VerifyPassword(password, usuario.Contraseña))
                {
                    return usuario;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en autenticación: " + ex.Message);
            }
        }

        public int CrearUsuario(Usuario usuario, string password)
        {
            ValidateUsuario(usuario);

            try
            {
                usuario.Contraseña = HashPassword(password);
                return _usuarioRepository.Insert(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario: " + ex.Message);
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            ValidateUsuario(usuario);

            try
            {
                return _usuarioRepository.Update(usuario) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }

        public bool CambiarPassword(int usuarioId, string newPassword)
        {
            try
            {
                var hashedPassword = HashPassword(newPassword);
                return _usuarioRepository.UpdatePassword(usuarioId, hashedPassword) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar contraseña: " + ex.Message);
            }
        }

        private void ValidateUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre de usuario es requerido");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new Exception("El rol es requerido");

            if (usuario.Nombre.Length > 50)
                throw new Exception("El nombre no puede exceder 50 caracteres");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashOfInput = HashPassword(inputPassword);
            return string.Equals(hashOfInput, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}