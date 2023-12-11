using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empolyer_Form
{
    internal class Employer
    {
        private string name;
        private string gender;
        private string birth;
        private string fav_club;
        private string company;
        private string job;

        public string Name { get { return name; } set { name = value; } }
        public string Gender { get {  return gender; } set {  gender = value; } }
        public string Birth { get {  return birth; } set { birth = value; } }
        public string FavClub { get {  return fav_club; } set {  fav_club = value; } }
        public string Company { get { return company;} set { company = value; } }
        public string Job { get { return job; } set { job = value; } }
    }
}
