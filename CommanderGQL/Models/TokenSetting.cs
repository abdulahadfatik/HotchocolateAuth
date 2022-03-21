using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.Models
{
    public class TokenSetting
    {
        [Key]
        public int TokenId { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
