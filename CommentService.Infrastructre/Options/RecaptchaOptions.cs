using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentService.Infrastructure.Options
{
    public class RecaptchaOptions
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }

        public string Version { get; set; }

        public bool UseRecaptchaNet { get; set; }

        public double ScoreThreshold { get; set; }

        public string Domain { get; set; }

    }
}
