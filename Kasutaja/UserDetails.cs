using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino.Kasutaja
{
    public static class UserDetails
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static int KasutajaId { get; set; }
        public static string KasutajaNimi { get; set; }
        public static string Email { get; set; }
    }
}
