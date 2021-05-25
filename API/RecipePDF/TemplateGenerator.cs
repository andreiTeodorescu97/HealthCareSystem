using System.Text;
using API.DTOs.Recipes;

namespace API.RecipePDF
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            var employees = DataStorage.GetAllEmployees();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");
            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }

        public static string GetRecipeHTMLString(FullRecipeInfoDto fullRecipeInfoDto)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Reteta {0}</h1></div>
                                <div>
                                <div id='div1'>
                                    <li><strong>Eliberat pentru</strong></li>
                                    </br>
                                    <li>{1} {2}</li>
                                    <li>Email: {3}</li>
                                    <li>Serie: {4} Numar: {5}</li>
                                    <li>CNP: {6}</li>
                                </div>
                                <div id='div2'>
                                    <li><strong>Eliberat de catre:</strong></li>
                                    </br>
                                    <li>Dr {7} {8}</li>
                                    <li>Email: {9}</li>
                                    <li>La data de: {10}</li>
                                    <li>-</li>
                                </div>
                                </div>
                                </br>
                                <table align='center'>
                                    <tr>
                                        <th>Medicament</th>
                                        <th>Dozaj</th>
                                        <th>Frecventa</th>
                                        <th>Numar zile</th>
                                    </tr>", 
                                    fullRecipeInfoDto.Recipe.UniqueId, 
                                    fullRecipeInfoDto.Pacient.FirstName, 
                                    fullRecipeInfoDto.Pacient.SecondName,
                                    fullRecipeInfoDto.Pacient.Email,
                                    fullRecipeInfoDto.Pacient.Series,
                                    fullRecipeInfoDto.Pacient.IdentityNumber,
                                    fullRecipeInfoDto.Pacient.CNP,
                                    fullRecipeInfoDto.Doctor.FirstName,
                                    fullRecipeInfoDto.Doctor.SecondName,
                                    fullRecipeInfoDto.Doctor.Email,
                                    fullRecipeInfoDto.Recipe.DateAdded
                            );
                                    
            foreach (var med in fullRecipeInfoDto.Recipe.Prescriptions)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1} {2}</td>
                                    <td>{3} pe zi</td>
                                    <td>{4}</td>
                                  </tr>", med.Medicine.CommercialName, med.DosageNumberPerDay, med.DosageType.ToLower(), med.Frequency, med.NumberOfDays);
            }
            sb.Append(@"
                                </table>
                                </br>
                                </br>
                                </br>
                                <div>
                                <div id='div3'>
                                    <h2>Semnatura medic</h2>
                                </div>
                                <div id='div4'>
                                    <h2>Stampila medic</h2>
                                </div>
                                </div>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}