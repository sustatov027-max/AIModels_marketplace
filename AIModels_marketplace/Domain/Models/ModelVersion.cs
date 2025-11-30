using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Domain.Models
{
    public class ModelVersion
    {
        public string VersionNumber {  get; set; }
        public DateTime ReleaseDate { get;  set; }
        public string Changelog { get; set; }
    
        public ModelVersion(string versionNumber, string changelog) {
            VersionNumber = versionNumber;
            ReleaseDate = DateTime.Now;
            Changelog = changelog;
        }
    }
}
