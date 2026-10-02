namespace ConstruSys.Application.Helpers
{
    public static class TextoHelper
    {
        public static string CaixaAlta(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? string.Empty
                : valor.Trim().ToUpperInvariant();
        }

        public static string? CaixaAltaOuNull(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim().ToUpperInvariant();
        }

        public static string? MinusculoOuNull(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim().ToLowerInvariant();
        }

        public static string? ApenasTrimOuNull(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }
    }
}