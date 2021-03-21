using System.Collections.Generic;

namespace Backend.Repository {
    public class Database : IDatabase {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryInhabitantsRepository _countryDetailedRepository;

        public Database(ICountryRepository countryRepository, ICountryInhabitantsRepository countryDetailedRepository) {
            _countryRepository = countryRepository;
            _countryDetailedRepository = countryDetailedRepository;
        }

        public void Initialize(IUnitOfWork unitOfWork) {
            DeleteAllTables(unitOfWork);
            CreateTables(unitOfWork);
            InsertCountries(unitOfWork);
        }

        private void InsertCountries(IUnitOfWork unitOfWork) {
            var countries = new List<CountryDao> {
                new CountryDao {
                    Id = CountryType.Afghanistan,
                    Name = "Afghanistan"
                },
                new CountryDao {
                    Id = CountryType.Albania,
                    Name = "Albania"
                },
                new CountryDao {
                    Id = CountryType.Algeria,
                    Name = "Algeria"
                },
                new CountryDao {
                    Id = CountryType.Andorra,
                    Name = "Andorra"
                },
                new CountryDao {
                    Id = CountryType.Angola,
                    Name = "Angola"
                },
                new CountryDao {
                    Id = CountryType.AntiguaAndBarbuda,
                    Name = "Antigua and Barbuda"
                },
                new CountryDao {
                    Id = CountryType.Argentina,
                    Name = "Argentina"
                },
                new CountryDao {
                    Id = CountryType.Armenia,
                    Name = "Armenia"
                },
                new CountryDao {
                    Id = CountryType.Australia,
                    Name = "Australia"
                },
                new CountryDao {
                    Id = CountryType.Austria,
                    Name = "Austria"
                },
                new CountryDao {
                    Id = CountryType.Azerbaijan,
                    Name = "Azerbaijan"
                },
                new CountryDao {
                    Id = CountryType.Bahamas,
                    Name = "Bahamas"
                },
                new CountryDao {
                    Id = CountryType.Bahrain,
                    Name = "Bahrain"
                },
                new CountryDao {
                    Id = CountryType.Bangladesh,
                    Name = "Bangladesh"
                },
                new CountryDao {
                    Id = CountryType.Barbados,
                    Name = "Barbados"
                },
                new CountryDao {
                    Id = CountryType.Belarus,
                    Name = "Belarus"
                },
                new CountryDao {
                    Id = CountryType.Belgium,
                    Name = "Belgium"
                },
                new CountryDao {
                    Id = CountryType.Belize,
                    Name = "Belize"
                },
                new CountryDao {
                    Id = CountryType.Benin,
                    Name = "Benin"
                },
                new CountryDao {
                    Id = CountryType.Bhutan,
                    Name = "Bhutan"
                },
                new CountryDao {
                    Id = CountryType.Bolivia,
                    Name = "Bolivia"
                },
                new CountryDao {
                    Id = CountryType.BosniaAndHerzegovina,
                    Name = "Bosnia and Herzegovina"
                },
                new CountryDao {
                    Id = CountryType.Brazil,
                    Name = "Brazil"
                },
                new CountryDao {
                    Id = CountryType.Brunei,
                    Name = "Brunei"
                },
                new CountryDao {
                    Id = CountryType.Bulgaria,
                    Name = "Bulgaria"
                },
                new CountryDao {
                    Id = CountryType.BurkinaFaso,
                    Name = "Faso"
                },
                new CountryDao {
                    Id = CountryType.Burma,
                    Name = "Burma"
                },
                new CountryDao {
                    Id = CountryType.CaboVerde,
                    Name = "CaboVerde"
                },
                new CountryDao {
                    Id = CountryType.Cambodia,
                    Name = "Cambodia"
                },
                new CountryDao {
                    Id = CountryType.Cameroon,
                    Name = "Cameroon"
                },
                new CountryDao {
                    Id = CountryType.Canada,
                    Name = "Canada"
                },
                new CountryDao {
                    Id = CountryType.CentralAfricanRepublic,
                    Name = "Central African Republic"
                },
                new CountryDao {
                    Id = CountryType.Chad,
                    Name = "Chad"
                },
                new CountryDao {
                    Id = CountryType.Chile,
                    Name = "Chile"
                },
                new CountryDao {
                    Id = CountryType.China,
                    Name = "China"
                },
                new CountryDao {
                    Id = CountryType.Colombia,
                    Name = "Colombia"
                },
                new CountryDao {
                    Id = CountryType.CongoBrazzaville,
                    Name = "Congo (Brazzaville)"
                },
                new CountryDao {
                    Id = CountryType.CongoKinshasa,
                    Name = "Congo (Kinshasa)"
                },
                new CountryDao {
                    Id = CountryType.CostaRica,
                    Name = "Costa Rica"
                },
                new CountryDao {
                    Id = CountryType.CotedIvoire,
                    Name = "Cote d'Ivoire"
                },
                new CountryDao {
                    Id = CountryType.Croatia,
                    Name = "Croatia"
                },
                new CountryDao {
                    Id = CountryType.Cuba,
                    Name = "Cuba"
                },
                new CountryDao {
                    Id = CountryType.Cyprus,
                    Name = "Cyprus"
                },
                new CountryDao {
                    Id = CountryType.Czechia,
                    Name = "Czechia"
                },
                new CountryDao {
                    Id = CountryType.Denmark,
                    Name = "Denmark"
                },
                new CountryDao {
                    Id = CountryType.Others,
                    Name = "Others"
                },
                new CountryDao {
                    Id = CountryType.Djibouti,
                    Name = "Djibouti"
                },
                new CountryDao {
                    Id = CountryType.Dominica,
                    Name = "Dominica"
                },
                new CountryDao {
                    Id = CountryType.DominicanRepublic,
                    Name = "Dominican Republic"
                },
                new CountryDao {
                    Id = CountryType.Ecuador,
                    Name = "Ecuador"
                },
                new CountryDao {
                    Id = CountryType.Egypt,
                    Name = "Egypt"
                },
                new CountryDao {
                    Id = CountryType.ElSalvador,
                    Name = "El Salvador"
                },
                new CountryDao {
                    Id = CountryType.EquatorialGuinea,
                    Name = "Equatorial Guinea"
                },
                new CountryDao {
                    Id = CountryType.Eritrea,
                    Name = "Eritrea"
                },
                new CountryDao {
                    Id = CountryType.Estonia,
                    Name = "Estonia"
                },
                new CountryDao {
                    Id = CountryType.Eswatini,
                    Name = "Eswatini"
                },
                new CountryDao {
                    Id = CountryType.Ethiopia,
                    Name = "Ethiopia"
                },
                new CountryDao {
                    Id = CountryType.Fiji,
                    Name = "Fiji"
                },
                new CountryDao {
                    Id = CountryType.Finland,
                    Name = "Finland"
                },
                new CountryDao {
                    Id = CountryType.France,
                    Name = "France"
                },
                new CountryDao {
                    Id = CountryType.Gabon,
                    Name = "Gabon"
                },
                new CountryDao {
                    Id = CountryType.Gambia,
                    Name = "Gambia"
                },
                new CountryDao {
                    Id = CountryType.Georgia,
                    Name = "Georgia"
                },
                new CountryDao {
                    Id = CountryType.Germany,
                    Name = "Germany"
                },
                new CountryDao {
                    Id = CountryType.Ghana,
                    Name = "Ghana"
                },
                new CountryDao {
                    Id = CountryType.Greece,
                    Name = "Greece"
                },
                new CountryDao {
                    Id = CountryType.Grenada,
                    Name = "Grenada"
                },
                new CountryDao {
                    Id = CountryType.Guatemala,
                    Name = "Guatemala"
                },
                new CountryDao {
                    Id = CountryType.Guinea,
                    Name = "Guinea"
                },
                new CountryDao {
                    Id = CountryType.GuineaBissau,
                    Name = "Guinea-Bissau"
                },
                new CountryDao {
                    Id = CountryType.Guyana,
                    Name = "Guyana"
                },
                new CountryDao {
                    Id = CountryType.Haiti,
                    Name = "Haiti"
                },
                new CountryDao {
                    Id = CountryType.HolySee,
                    Name = "Holy See"
                },
                new CountryDao {
                    Id = CountryType.Honduras,
                    Name = "Honduras"
                },
                new CountryDao {
                    Id = CountryType.Hungary,
                    Name = "Hungary"
                },
                new CountryDao {
                    Id = CountryType.Iceland,
                    Name = "Iceland"
                },
                new CountryDao {
                    Id = CountryType.India,
                    Name = "India"
                },
                new CountryDao {
                    Id = CountryType.Indonesia,
                    Name = "Indonesia"
                },
                new CountryDao {
                    Id = CountryType.Iran,
                    Name = "Iran"
                },
                new CountryDao {
                    Id = CountryType.Iraq,
                    Name = "Iraq"
                },
                new CountryDao {
                    Id = CountryType.Ireland,
                    Name = "Ireland"
                },
                new CountryDao {
                    Id = CountryType.Israel,
                    Name = "Israel"
                },
                new CountryDao {
                    Id = CountryType.Italy,
                    Name = "Italy"
                },
                new CountryDao {
                    Id = CountryType.Jamaica,
                    Name = "Jamaica"
                },
                new CountryDao {
                    Id = CountryType.Japan,
                    Name = "Japan"
                },
                new CountryDao {
                    Id = CountryType.Jordan,
                    Name = "Jordan"
                },
                new CountryDao {
                    Id = CountryType.Kazakhstan,
                    Name = "Kazakhstan"
                },
                new CountryDao {
                    Id = CountryType.Kenya,
                    Name = "Kenya"
                },
                new CountryDao {
                    Id = CountryType.SouthKorea,
                    Name = "South Korea"
                },
                new CountryDao {
                    Id = CountryType.Kosovo,
                    Name = "Kosovo"
                },
                new CountryDao {
                    Id = CountryType.Kuwait,
                    Name = "Kuwait"
                },
                new CountryDao {
                    Id = CountryType.Kyrgyzstan,
                    Name = "Kyrgyzstan"
                },
                new CountryDao {
                    Id = CountryType.Laos,
                    Name = "Laos"
                },
                new CountryDao {
                    Id = CountryType.Latvia,
                    Name = "Latvia"
                },
                new CountryDao {
                    Id = CountryType.Lebanon,
                    Name = "Lebanon"
                },
                new CountryDao {
                    Id = CountryType.Liberia,
                    Name = "Liberia"
                },
                new CountryDao {
                    Id = CountryType.Libya,
                    Name = "Libya"
                },
                new CountryDao {
                    Id = CountryType.Liechtenstein,
                    Name = "Liechtenstein"
                },
                new CountryDao {
                    Id = CountryType.Lithuania,
                    Name = "Lithuania"
                },
                new CountryDao {
                    Id = CountryType.Luxembourg,
                    Name = "Luxembourg"
                },
                new CountryDao {
                    Id = CountryType.Madagascar,
                    Name = "Madagascar"
                },
                new CountryDao {
                    Id = CountryType.Malaysia,
                    Name = "Malaysia"
                },
                new CountryDao {
                    Id = CountryType.Maldives,
                    Name = "Maldives"
                },
                new CountryDao {
                    Id = CountryType.Mali,
                    Name = "Mali"
                },
                new CountryDao {
                    Id = CountryType.Malta,
                    Name = "Malta"
                },
                new CountryDao {
                    Id = CountryType.Mauritania,
                    Name = "Mauritania"
                },
                new CountryDao {
                    Id = CountryType.Mauritius,
                    Name = "Mauritius"
                },
                new CountryDao {
                    Id = CountryType.Mexico,
                    Name = "Mexico"
                },
                new CountryDao {
                    Id = CountryType.Moldova,
                    Name = "Moldova"
                },
                new CountryDao {
                    Id = CountryType.Monaco,
                    Name = "Monaco"
                },
                new CountryDao {
                    Id = CountryType.Mongolia,
                    Name = "Mongolia"
                },
                new CountryDao {
                    Id = CountryType.Montenegro,
                    Name = "Montenegro"
                },
                new CountryDao {
                    Id = CountryType.Morocco,
                    Name = "Morocco"
                },
                new CountryDao {
                    Id = CountryType.Mozambique,
                    Name = "Mozambique"
                },
                new CountryDao {
                    Id = CountryType.Namibia,
                    Name = "Namibia"
                },
                new CountryDao {
                    Id = CountryType.Nepal,
                    Name = "Nepal"
                },
                new CountryDao {
                    Id = CountryType.Netherlands,
                    Name = "Netherlands"
                },
                new CountryDao {
                    Id = CountryType.NewZealand,
                    Name = "New Zealand"
                },
                new CountryDao {
                    Id = CountryType.Nicaragua,
                    Name = "Nicaragua"
                },
                new CountryDao {
                    Id = CountryType.Niger,
                    Name = "Niger"
                },
                new CountryDao {
                    Id = CountryType.Nigeria,
                    Name = "Nigeria"
                },
                new CountryDao {
                    Id = CountryType.NorthMacedonia,
                    Name = "North Macedonia"
                },
                new CountryDao {
                    Id = CountryType.Norway,
                    Name = "Norway"
                },
                new CountryDao {
                    Id = CountryType.Oman,
                    Name = "Oman"
                },
                new CountryDao {
                    Id = CountryType.Pakistan,
                    Name = "Pakistan"
                },
                new CountryDao {
                    Id = CountryType.Panama,
                    Name = "Panama"
                },
                new CountryDao {
                    Id = CountryType.PapuaNewGuinea,
                    Name = "Papua New Guinea"
                },
                new CountryDao {
                    Id = CountryType.Paraguay,
                    Name = "Paraguay"
                },
                new CountryDao {
                    Id = CountryType.Peru,
                    Name = "Peru"
                },
                new CountryDao {
                    Id = CountryType.Philippines,
                    Name = "Philippines"
                },
                new CountryDao {
                    Id = CountryType.Poland,
                    Name = "Poland"
                },
                new CountryDao {
                    Id = CountryType.Portugal,
                    Name = "Portugal"
                },
                new CountryDao {
                    Id = CountryType.Qatar,
                    Name = "Qatar"
                },
                new CountryDao {
                    Id = CountryType.Romania,
                    Name = "Romania"
                },
                new CountryDao {
                    Id = CountryType.Russia,
                    Name = "Russia"
                },
                new CountryDao {
                    Id = CountryType.Rwanda,
                    Name = "Rwanda"
                },
                new CountryDao {
                    Id = CountryType.SaintKittsAndNevis,
                    Name = "Saint Kitts and Nevis"
                },
                new CountryDao {
                    Id = CountryType.SaintLucia,
                    Name = "Saitn Lucia"
                },
                new CountryDao {
                    Id = CountryType.SaintVincentAndTheGrenadines,
                    Name = "Saint Vincent and the Grenadines"
                },
                new CountryDao {
                    Id = CountryType.SanMarino,
                    Name = "San Marino"
                },
                new CountryDao {
                    Id = CountryType.SaudiArabia,
                    Name = "Saudi Arabia"
                },
                new CountryDao {
                    Id = CountryType.Senegal,
                    Name = "Senegal"
                },
                new CountryDao {
                    Id = CountryType.Serbia,
                    Name = "Serbia"
                },
                new CountryDao {
                    Id = CountryType.Seychelles,
                    Name = "Seychelles"
                },
                new CountryDao {
                    Id = CountryType.Singapore,
                    Name = "Singapore"
                },
                new CountryDao {
                    Id = CountryType.Slovakia,
                    Name = "Slovakia"
                },
                new CountryDao {
                    Id = CountryType.Slovenia,
                    Name = "Slovenia"
                },
                new CountryDao {
                    Id = CountryType.Somalia,
                    Name = "Somalia"
                },
                new CountryDao {
                    Id = CountryType.SouthAfrica,
                    Name = "South Africa"
                },
                new CountryDao {
                    Id = CountryType.Spain,
                    Name = "Spain"
                },
                new CountryDao {
                    Id = CountryType.SriLanka,
                    Name = "Sri Lanka"
                },
                new CountryDao {
                    Id = CountryType.Sudan,
                    Name = "Sudan"
                },
                new CountryDao {
                    Id = CountryType.Suriname,
                    Name = "Suriname"
                },
                new CountryDao {
                    Id = CountryType.Sweden,
                    Name = "Sweden"
                },
                new CountryDao {
                    Id = CountryType.Switzerland,
                    Name = "Switzerland"
                },
                new CountryDao {
                    Id = CountryType.Syria,
                    Name = "Syria"
                },
                new CountryDao {
                    Id = CountryType.Taiwan,
                    Name = "Taiwan"
                },
                new CountryDao {
                    Id = CountryType.Tanzania,
                    Name = "Tanzania"
                },
                new CountryDao {
                    Id = CountryType.Thailand,
                    Name = "Thailand"
                },
                new CountryDao {
                    Id = CountryType.TimorLeste,
                    Name = "Timor-Leste"
                },
                new CountryDao {
                    Id = CountryType.Togo,
                    Name = "Togo"
                },
                new CountryDao {
                    Id = CountryType.TrinidadAndTobago,
                    Name = "Trinidad and Tobago"
                },
                new CountryDao {
                    Id = CountryType.Tunisia,
                    Name = "Tunisia"
                },
                new CountryDao {
                    Id = CountryType.Turkey,
                    Name = "Turkey"
                },
                new CountryDao {
                    Id = CountryType.Usa,
                    Name = "USA"
                },
                new CountryDao {
                    Id = CountryType.Uganda,
                    Name = "Uganda"
                },
                new CountryDao {
                    Id = CountryType.Ukraine,
                    Name = "Ukraine"
                },
                new CountryDao {
                    Id = CountryType.UnitedArabEmirates,
                    Name = "United Arab Emirates"
                },
                new CountryDao {
                    Id = CountryType.UnitedKingdom,
                    Name = "United Kingdom"
                },
                new CountryDao {
                    Id = CountryType.Uruguay,
                    Name = "Uruguay"
                },
                new CountryDao {
                    Id = CountryType.Uzbekistan,
                    Name = "Uzbekistan"
                },
                new CountryDao {
                    Id = CountryType.Venezuela,
                    Name = "Venezuela"
                },
                new CountryDao {
                    Id = CountryType.Vietnam,
                    Name = "Vietnam"
                },
                new CountryDao {
                    Id = CountryType.WestBankAndGaza,
                    Name = "West Bank and Gaza"
                },
                new CountryDao {
                    Id = CountryType.Zambia,
                    Name = "Zambia"
                },
                new CountryDao {
                    Id = CountryType.Zimbabwe,
                    Name = "Zimbabwe"
                },
                new CountryDao {
                    Id = CountryType.Gibraltar,
                    Name = "Gibraltar"
                },
                new CountryDao {
                    Id = CountryType.Vatican,
                    Name = "Vatican"
                },
                new CountryDao {
                    Id = CountryType.FaroeIslands,
                    Name = "Faroe Islands"
                },
                new CountryDao {
                    Id = CountryType.FrenchGuiana,
                    Name = "French Guiana"
                },
                new CountryDao {
                    Id = CountryType.SaintBarthelemy,
                    Name = "Saint Barthelemy"
                },
                new CountryDao {
                    Id = CountryType.Martinique,
                    Name = "Martinique"
                },
                new CountryDao {
                    Id = CountryType.SaintMartin,
                    Name = "Saint Martin"
                },
                new CountryDao {
                    Id = CountryType.ChannelIslands,
                    Name = "Channel Islands"
                },
                new CountryDao {
                    Id = CountryType.CaymanIslands,
                    Name = "Cayman Islands"
                },
                new CountryDao {
                    Id = CountryType.Guadeloupe,
                    Name = "Guadeloupe"
                },
                new CountryDao {
                    Id = CountryType.Aruba,
                    Name = "Aruba"
                },
                new CountryDao {
                    Id = CountryType.RepublicOfCongo,
                    Name = "Republic of Congo"
                },
                new CountryDao {
                    Id = CountryType.Mayotte,
                    Name = "Mayotte"
                },
                new CountryDao {
                    Id = CountryType.Reunion,
                    Name = "Reunion"
                },
                new CountryDao {
                    Id = CountryType.Greenland,
                    Name = "Greenland"
                },
                new CountryDao {
                    Id = CountryType.Guernsey,
                    Name = "Guernsey"
                },
                new CountryDao {
                    Id = CountryType.Curacao,
                    Name = "Curacao"
                },
                new CountryDao {
                    Id = CountryType.Burundi,
                    Name = "Burundi"
                },
                new CountryDao {
                    Id = CountryType.Botswana,
                    Name = "Botswana"
                },
                new CountryDao {
                    Id = CountryType.SierraLeone,
                    Name = "SierraLeone"
                },
                new CountryDao {
                    Id = CountryType.Malawi,
                    Name = "Malawi"
                },
                new CountryDao {
                    Id = CountryType.SaoTomeAndPrincipe,
                    Name = "Sao Tome and Principe"
                },
                new CountryDao {
                    Id = CountryType.SouthSudan,
                    Name = "South Sudan"
                },
                new CountryDao {
                    Id = CountryType.WesternSahara,
                    Name = "Western Sahara"
                },
                new CountryDao {
                    Id = CountryType.Yemen,
                    Name = "Yemen"
                },
                new CountryDao {
                    Id = CountryType.Comoros,
                    Name = "Comoros"
                },
                new CountryDao {
                    Id = CountryType.Lesotho,
                    Name = "Lesotho"
                },
                new CountryDao {
                    Id = CountryType.Tajikistan,
                    Name = "Tajikistan"
                },
                new CountryDao {
                    Id = CountryType.SolomonIslands,
                    Name = "Solomon Islands"
                },
                new CountryDao {
                    Id = CountryType.Vanuatu,
                    Name = "Vanuatu"
                },
                new CountryDao {
                    Id = CountryType.Samoa,
                    Name = "Samoa"
                },
                new CountryDao {
                    Id = CountryType.MarshallIslands,
                    Name = "Marshall Islands"
                },
                new CountryDao {
                    Id = CountryType.Micronesia,
                    Name = "Micronesia"
                }
            };

            var countriesDetailed = new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao { CountryId = CountryType.China, Inhabitants = 1402059200 },
                new CountryInhabitantsDao { CountryId = CountryType.India, Inhabitants = 1360617526 },
                new CountryInhabitantsDao { CountryId = CountryType.Usa, Inhabitants = 329567756 },
                new CountryInhabitantsDao { CountryId = CountryType.Indonesia, Inhabitants = 266911900 },
                new CountryInhabitantsDao { CountryId = CountryType.Pakistan, Inhabitants = 219170520 },
                new CountryInhabitantsDao { CountryId = CountryType.Brazil, Inhabitants = 211345539 },
                new CountryInhabitantsDao { CountryId = CountryType.Nigeria, Inhabitants = 206139587 },
                new CountryInhabitantsDao { CountryId = CountryType.Bangladesh, Inhabitants = 168383172 },
                new CountryInhabitantsDao { CountryId = CountryType.Russia, Inhabitants = 146745098 },
                new CountryInhabitantsDao { CountryId = CountryType.Mexico, Inhabitants = 126577691 },
                new CountryInhabitantsDao { CountryId = CountryType.Japan, Inhabitants = 125950000 },
                new CountryInhabitantsDao { CountryId = CountryType.Philippines, Inhabitants = 108495271 },
                new CountryInhabitantsDao { CountryId = CountryType.Egypt, Inhabitants = 100207983 },
                new CountryInhabitantsDao { CountryId = CountryType.Ethiopia, Inhabitants = 98665000 },
                new CountryInhabitantsDao { CountryId = CountryType.Vietnam, Inhabitants = 96208984 },
                new CountryInhabitantsDao { CountryId = CountryType.CongoKinshasa, Inhabitants = 89561404 },
                new CountryInhabitantsDao { CountryId = CountryType.Iran, Inhabitants = 83336520 },
                new CountryInhabitantsDao { CountryId = CountryType.Turkey, Inhabitants = 83154997 },
                new CountryInhabitantsDao { CountryId = CountryType.Germany, Inhabitants = 83149300 },
                new CountryInhabitantsDao { CountryId = CountryType.France, Inhabitants = 67076000 },
                new CountryInhabitantsDao { CountryId = CountryType.Thailand, Inhabitants = 66489380 },
                new CountryInhabitantsDao { CountryId = CountryType.UnitedKingdom, Inhabitants = 66435550 },
                new CountryInhabitantsDao { CountryId = CountryType.Italy, Inhabitants = 60243406 },
                new CountryInhabitantsDao { CountryId = CountryType.SouthAfrica, Inhabitants = 58775022 },
                new CountryInhabitantsDao { CountryId = CountryType.Tanzania, Inhabitants = 55890747 },
                new CountryInhabitantsDao { CountryId = CountryType.Burma, Inhabitants = 54339766 },
                new CountryInhabitantsDao { CountryId = CountryType.SouthKorea, Inhabitants = 51780579 },
                new CountryInhabitantsDao { CountryId = CountryType.Colombia, Inhabitants = 49395678 },
                new CountryInhabitantsDao { CountryId = CountryType.Kenya, Inhabitants = 47564296 },
                new CountryInhabitantsDao { CountryId = CountryType.Spain, Inhabitants = 47100396 },
                new CountryInhabitantsDao { CountryId = CountryType.Argentina, Inhabitants = 44938712 },
                new CountryInhabitantsDao { CountryId = CountryType.Algeria, Inhabitants = 43000000 },
                new CountryInhabitantsDao { CountryId = CountryType.Sudan, Inhabitants = 42398410 },
                new CountryInhabitantsDao { CountryId = CountryType.Uganda, Inhabitants = 40299300 },
                new CountryInhabitantsDao { CountryId = CountryType.Iraq, Inhabitants = 39127900 },
                new CountryInhabitantsDao { CountryId = CountryType.Poland, Inhabitants = 38379000 },
                new CountryInhabitantsDao { CountryId = CountryType.Canada, Inhabitants = 37981433 },
                new CountryInhabitantsDao { CountryId = CountryType.Morocco, Inhabitants = 35858631 },
                new CountryInhabitantsDao { CountryId = CountryType.SaudiArabia, Inhabitants = 34218169 },
                new CountryInhabitantsDao { CountryId = CountryType.Uzbekistan, Inhabitants = 34107457 },
                new CountryInhabitantsDao { CountryId = CountryType.Malaysia, Inhabitants = 32739760 },
                new CountryInhabitantsDao { CountryId = CountryType.Afghanistan, Inhabitants = 32225560 },
                new CountryInhabitantsDao { CountryId = CountryType.Venezuela, Inhabitants = 32219521 },
                new CountryInhabitantsDao { CountryId = CountryType.Peru, Inhabitants = 32131400 },
                new CountryInhabitantsDao { CountryId = CountryType.Angola, Inhabitants = 31127674 },
                new CountryInhabitantsDao { CountryId = CountryType.Ghana, Inhabitants = 30280811 },
                new CountryInhabitantsDao { CountryId = CountryType.Mozambique, Inhabitants = 30066648 },
                new CountryInhabitantsDao { CountryId = CountryType.Nepal, Inhabitants = 29996478 },
                new CountryInhabitantsDao { CountryId = CountryType.Cameroon, Inhabitants = 26545864 },
                new CountryInhabitantsDao { CountryId = CountryType.Madagascar, Inhabitants = 26251309 },
                new CountryInhabitantsDao { CountryId = CountryType.CotedIvoire, Inhabitants = 25823071 },
                new CountryInhabitantsDao { CountryId = CountryType.Australia, Inhabitants = 25667395 },
                new CountryInhabitantsDao { CountryId = CountryType.Taiwan, Inhabitants = 23604265 },
                new CountryInhabitantsDao { CountryId = CountryType.Niger, Inhabitants = 22314743 },
                new CountryInhabitantsDao { CountryId = CountryType.SriLanka, Inhabitants = 21803000 },
                new CountryInhabitantsDao { CountryId = CountryType.BurkinaFaso, Inhabitants = 20870060 },
                new CountryInhabitantsDao { CountryId = CountryType.Mali, Inhabitants = 19973000 },
                new CountryInhabitantsDao { CountryId = CountryType.Romania, Inhabitants = 19405156 },
                new CountryInhabitantsDao { CountryId = CountryType.Malawi, Inhabitants = 19129952 },
                new CountryInhabitantsDao { CountryId = CountryType.Chile, Inhabitants = 19107216 },
                new CountryInhabitantsDao { CountryId = CountryType.Kazakhstan, Inhabitants = 18675704 },
                new CountryInhabitantsDao { CountryId = CountryType.Zambia, Inhabitants = 17885422 },
                new CountryInhabitantsDao { CountryId = CountryType.Syria, Inhabitants = 17500657 },
                new CountryInhabitantsDao { CountryId = CountryType.Ecuador, Inhabitants = 17458076 },
                new CountryInhabitantsDao { CountryId = CountryType.Netherlands, Inhabitants = 17451731 },
                new CountryInhabitantsDao { CountryId = CountryType.Guatemala, Inhabitants = 16604026 },
                new CountryInhabitantsDao { CountryId = CountryType.Chad, Inhabitants = 16244513 },
                new CountryInhabitantsDao { CountryId = CountryType.Senegal, Inhabitants = 16209125 },
                new CountryInhabitantsDao { CountryId = CountryType.Somalia, Inhabitants = 15893219 },
                new CountryInhabitantsDao { CountryId = CountryType.Cambodia, Inhabitants = 15288489 },
                new CountryInhabitantsDao { CountryId = CountryType.Zimbabwe, Inhabitants = 15159624 },
                new CountryInhabitantsDao { CountryId = CountryType.Rwanda, Inhabitants = 12374397 },
                new CountryInhabitantsDao { CountryId = CountryType.Guinea, Inhabitants = 12218357 },
                new CountryInhabitantsDao { CountryId = CountryType.Benin, Inhabitants = 11733059 },
                new CountryInhabitantsDao { CountryId = CountryType.Tunisia, Inhabitants = 11722038 },
                new CountryInhabitantsDao { CountryId = CountryType.Haiti, Inhabitants = 11577779 },
                new CountryInhabitantsDao { CountryId = CountryType.Belgium, Inhabitants = 11524454 },
                new CountryInhabitantsDao { CountryId = CountryType.Bolivia, Inhabitants = 11469896 },
                new CountryInhabitantsDao { CountryId = CountryType.Cuba, Inhabitants = 11209628 },
                new CountryInhabitantsDao { CountryId = CountryType.Burundi, Inhabitants = 10953317 },
                new CountryInhabitantsDao { CountryId = CountryType.Greece, Inhabitants = 10724599 },
                new CountryInhabitantsDao { CountryId = CountryType.Czechia, Inhabitants = 10693939 },
                new CountryInhabitantsDao { CountryId = CountryType.Jordan, Inhabitants = 10650844 },
                new CountryInhabitantsDao { CountryId = CountryType.DominicanRepublic, Inhabitants = 10358320 },
                new CountryInhabitantsDao { CountryId = CountryType.Sweden, Inhabitants = 10333456 },
                new CountryInhabitantsDao { CountryId = CountryType.Portugal, Inhabitants = 10276617 },
                new CountryInhabitantsDao { CountryId = CountryType.Azerbaijan, Inhabitants = 10067108 },
                new CountryInhabitantsDao { CountryId = CountryType.UnitedArabEmirates, Inhabitants = 9890400 },
                new CountryInhabitantsDao { CountryId = CountryType.Hungary, Inhabitants = 9772756 },
                new CountryInhabitantsDao { CountryId = CountryType.Belarus, Inhabitants = 9413446 },
                new CountryInhabitantsDao { CountryId = CountryType.Israel, Inhabitants = 9180900 },
                new CountryInhabitantsDao { CountryId = CountryType.Honduras, Inhabitants = 9158345 },
                new CountryInhabitantsDao { CountryId = CountryType.PapuaNewGuinea, Inhabitants = 8935000 },
                new CountryInhabitantsDao { CountryId = CountryType.Austria, Inhabitants = 8902600 },
                new CountryInhabitantsDao { CountryId = CountryType.Switzerland, Inhabitants = 8586550 },
                new CountryInhabitantsDao { CountryId = CountryType.SierraLeone, Inhabitants = 7901454 },
                new CountryInhabitantsDao { CountryId = CountryType.Togo, Inhabitants = 7538000 },
                new CountryInhabitantsDao { CountryId = CountryType.Paraguay, Inhabitants = 7152703 },
                new CountryInhabitantsDao { CountryId = CountryType.Laos, Inhabitants = 7123205 },
                new CountryInhabitantsDao { CountryId = CountryType.Bulgaria, Inhabitants = 7000039 },
                new CountryInhabitantsDao { CountryId = CountryType.Serbia, Inhabitants = 6963764 },
                new CountryInhabitantsDao { CountryId = CountryType.Libya, Inhabitants = 6871287 },
                new CountryInhabitantsDao { CountryId = CountryType.Lebanon, Inhabitants = 6825442 },
                new CountryInhabitantsDao { CountryId = CountryType.Kyrgyzstan, Inhabitants = 6533500 },
                new CountryInhabitantsDao { CountryId = CountryType.ElSalvador, Inhabitants = 6486201 },
                new CountryInhabitantsDao { CountryId = CountryType.Nicaragua, Inhabitants = 6460411 },
                new CountryInhabitantsDao { CountryId = CountryType.Denmark, Inhabitants = 5822763 },
                new CountryInhabitantsDao { CountryId = CountryType.Singapore, Inhabitants = 5703600 },
                new CountryInhabitantsDao { CountryId = CountryType.Finland, Inhabitants = 5527573 },
                new CountryInhabitantsDao { CountryId = CountryType.CongoBrazzaville, Inhabitants = 5518092 },
                new CountryInhabitantsDao { CountryId = CountryType.CentralAfricanRepublic, Inhabitants = 5496011 },
                new CountryInhabitantsDao { CountryId = CountryType.Slovakia, Inhabitants = 5456362 },
                new CountryInhabitantsDao { CountryId = CountryType.Norway, Inhabitants = 5367580 },
                new CountryInhabitantsDao { CountryId = CountryType.CostaRica, Inhabitants = 5058007 },
                new CountryInhabitantsDao { CountryId = CountryType.WestBankAndGaza, Inhabitants = 4976684 },
                new CountryInhabitantsDao { CountryId = CountryType.NewZealand, Inhabitants = 4975500 },
                new CountryInhabitantsDao { CountryId = CountryType.Ireland, Inhabitants = 4921500 },
                new CountryInhabitantsDao { CountryId = CountryType.Oman, Inhabitants = 4664790 },
                new CountryInhabitantsDao { CountryId = CountryType.Liberia, Inhabitants = 4475353 },
                new CountryInhabitantsDao { CountryId = CountryType.Kuwait, Inhabitants = 4420110 },
                new CountryInhabitantsDao { CountryId = CountryType.Panama, Inhabitants = 4218808 },
                new CountryInhabitantsDao { CountryId = CountryType.Mauritania, Inhabitants = 4077347 },
                new CountryInhabitantsDao { CountryId = CountryType.Croatia, Inhabitants = 4076246 },
                new CountryInhabitantsDao { CountryId = CountryType.Georgia, Inhabitants = 3723464 },
                new CountryInhabitantsDao { CountryId = CountryType.Uruguay, Inhabitants = 3518552 },
                new CountryInhabitantsDao { CountryId = CountryType.Eritrea, Inhabitants = 3497117 },
                new CountryInhabitantsDao { CountryId = CountryType.Mongolia, Inhabitants = 3310918 },
                new CountryInhabitantsDao { CountryId = CountryType.BosniaAndHerzegovina, Inhabitants = 3301000 },
                new CountryInhabitantsDao { CountryId = CountryType.Armenia, Inhabitants = 2957500 },
                new CountryInhabitantsDao { CountryId = CountryType.Albania, Inhabitants = 2845955 },
                new CountryInhabitantsDao { CountryId = CountryType.Lithuania, Inhabitants = 2793471 },
                new CountryInhabitantsDao { CountryId = CountryType.Qatar, Inhabitants = 2747282 },
                new CountryInhabitantsDao { CountryId = CountryType.Jamaica, Inhabitants = 2726667 },
                new CountryInhabitantsDao { CountryId = CountryType.Moldova, Inhabitants = 2681735 },
                new CountryInhabitantsDao { CountryId = CountryType.Namibia, Inhabitants = 2458936 },
                new CountryInhabitantsDao { CountryId = CountryType.Gambia, Inhabitants = 2347706 },
                new CountryInhabitantsDao { CountryId = CountryType.Botswana, Inhabitants = 2338851 },
                new CountryInhabitantsDao { CountryId = CountryType.Gabon, Inhabitants = 2172579 },
                new CountryInhabitantsDao { CountryId = CountryType.Slovenia, Inhabitants = 2094060 },
                new CountryInhabitantsDao { CountryId = CountryType.NorthMacedonia, Inhabitants = 2077132 },
                new CountryInhabitantsDao { CountryId = CountryType.Latvia, Inhabitants = 1906800 },
                new CountryInhabitantsDao { CountryId = CountryType.Kosovo, Inhabitants = 1795666 },
                new CountryInhabitantsDao { CountryId = CountryType.GuineaBissau, Inhabitants = 1604528 },
                new CountryInhabitantsDao { CountryId = CountryType.Bahrain, Inhabitants = 1543300 },
                new CountryInhabitantsDao { CountryId = CountryType.TimorLeste, Inhabitants = 1387149 },
                new CountryInhabitantsDao { CountryId = CountryType.TrinidadAndTobago, Inhabitants = 1363985 },
                new CountryInhabitantsDao { CountryId = CountryType.EquatorialGuinea, Inhabitants = 1358276 },
                new CountryInhabitantsDao { CountryId = CountryType.Estonia, Inhabitants = 1328360 },
                new CountryInhabitantsDao { CountryId = CountryType.Mauritius, Inhabitants = 1265985 },
                new CountryInhabitantsDao { CountryId = CountryType.Eswatini, Inhabitants = 1093238 },
                new CountryInhabitantsDao { CountryId = CountryType.Djibouti, Inhabitants = 1078373 },
                new CountryInhabitantsDao { CountryId = CountryType.Fiji, Inhabitants = 884887 },
                new CountryInhabitantsDao { CountryId = CountryType.Cyprus, Inhabitants = 875900 },
                new CountryInhabitantsDao { CountryId = CountryType.Guyana, Inhabitants = 782766 },
                new CountryInhabitantsDao { CountryId = CountryType.Bhutan, Inhabitants = 741672 },
                new CountryInhabitantsDao { CountryId = CountryType.Montenegro, Inhabitants = 622359 },
                new CountryInhabitantsDao { CountryId = CountryType.Luxembourg, Inhabitants = 613894 },
                new CountryInhabitantsDao { CountryId = CountryType.Suriname, Inhabitants = 581372 },
                new CountryInhabitantsDao { CountryId = CountryType.CaboVerde, Inhabitants = 550483 },
                new CountryInhabitantsDao { CountryId = CountryType.Malta, Inhabitants = 493559 },
                new CountryInhabitantsDao { CountryId = CountryType.Brunei, Inhabitants = 442400 },
                new CountryInhabitantsDao { CountryId = CountryType.Belize, Inhabitants = 408487 },
                new CountryInhabitantsDao { CountryId = CountryType.Bahamas, Inhabitants = 385340 },
                new CountryInhabitantsDao { CountryId = CountryType.Maldives, Inhabitants = 374775 },
                new CountryInhabitantsDao { CountryId = CountryType.Iceland, Inhabitants = 364260 },
                new CountryInhabitantsDao { CountryId = CountryType.Barbados, Inhabitants = 287025 },
                new CountryInhabitantsDao { CountryId = CountryType.SaintLucia, Inhabitants = 178696 },
                new CountryInhabitantsDao { CountryId = CountryType.Grenada, Inhabitants = 112003 },
                new CountryInhabitantsDao { CountryId = CountryType.SaintVincentAndTheGrenadines, Inhabitants = 110608 },
                new CountryInhabitantsDao { CountryId = CountryType.Seychelles, Inhabitants = 97625 },
                new CountryInhabitantsDao { CountryId = CountryType.AntiguaAndBarbuda, Inhabitants = 96453 },
                new CountryInhabitantsDao { CountryId = CountryType.Andorra, Inhabitants = 77543 },
                new CountryInhabitantsDao { CountryId = CountryType.Dominica, Inhabitants = 71808 },
                new CountryInhabitantsDao { CountryId = CountryType.SaintKittsAndNevis, Inhabitants = 52823 },
                new CountryInhabitantsDao { CountryId = CountryType.Liechtenstein, Inhabitants = 38749 },
                new CountryInhabitantsDao { CountryId = CountryType.Monaco, Inhabitants = 38300 },
                new CountryInhabitantsDao { CountryId = CountryType.SanMarino, Inhabitants = 33574 },
                new CountryInhabitantsDao { CountryId = CountryType.Vatican, Inhabitants = 799 }
            };

            unitOfWork.ExecuteDatabaseCommand(@"SET IDENTITY_INSERT Country ON");

            foreach (var country in countries) {
                _countryRepository.Insert(unitOfWork, country);
            }

            unitOfWork.ExecuteDatabaseCommand(@"SET IDENTITY_INSERT Country OFF");

            foreach (var country in countriesDetailed) {
                _countryDetailedRepository.Insert(unitOfWork, country);
            }
        }

        private void CreateTables(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE Country 
                (
                    Id INT PRIMARY KEY IDENTITY(1, 1),
                    Name NVARCHAR(100) NOT NULL
                )
");
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE CountryInhabitants 
                (
                    Id INT PRIMARY KEY IDENTITY(1, 1),
                    CountryId INT NOT NULL UNIQUE
                        CONSTRAINT FK_CountryInhabitants_Country_Id
                        REFERENCES Country(Id),
                    Inhabitants INT NOT NULL
                )
");
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE InfectionSpreadDataPoint 
                (
                    Id INT PRIMARY KEY NOT NULL IDENTITY,
                    [Date] DATETIME NOT NULL,
                    CountryId INT NOT NULL
                        CONSTRAINT FK_InfectionSpreadDataPoint_Country_Id
                        REFERENCES Country(Id),
                    InfectedTotal INT NOT NULL,
                    DeathsTotal INT NOT NULL,
                    RecoveredTotal INT NOT NULL
                )
");
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE ImportedCommitHistory 
                (
                    Id INT PRIMARY KEY NOT NULL IDENTITY,
                    [ImportTimestamp] DATETIME NOT NULL,
                    CommitHash NVARCHAR(100) NOT NULL
                )
");
        }

        private void DeleteAllTables(IUnitOfWork unitOfWork) {
            var tables = unitOfWork.QueryDatabase<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo'");
            var foreignKeyConstraints = unitOfWork.QueryDatabase<ForeignKeyConstraintAndTable>("SELECT fk.name as [ForeignKeyConstraint], t.name as [Table] FROM sys.foreign_keys fk JOIN sys.tables t on t.object_id = fk.parent_object_id");

            foreach (var foreignKeyConstraint in foreignKeyConstraints) {
                unitOfWork.ExecuteDatabaseCommand($"ALTER TABLE \"{foreignKeyConstraint.Table}\" DROP CONSTRAINT \"{foreignKeyConstraint.ForeignKeyConstraint}\"");
            }

            foreach (var table in tables) {
                unitOfWork.ExecuteDatabaseCommand($"DROP TABLE {table}");
            }
        }
    }
}
