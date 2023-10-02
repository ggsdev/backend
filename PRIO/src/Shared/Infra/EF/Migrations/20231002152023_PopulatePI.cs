using dotenv.net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class PopulatePI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var envVars = DotEnv.Read();

            string instanceKey = envVars["INSTANCE"];

            var databaseId = Guid.NewGuid();
            migrationBuilder.InsertData(
               table: "PI.Databases",
               columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute" },
               values: new object[] {
                    databaseId,
                    "F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T",
                    Guid.NewGuid().ToString(),
                    "PRIO - Cálculos",
                    "Database PRIO",
                    "https://prrjbsrvvm170.petrorio.local/piwebapi/assetdatabases/F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T",
                    "https://prrjbsrvvm170.petrorio.local/piwebapi/assetdatabases/F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T/elements"
               });

            if (instanceKey.ToUpper() == "BRAVO")
            {
                var bravoId = Guid.NewGuid();
                var polvoId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            bravoId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "34a482d8-ea8b-11ed-b18d-0050568cb915",
                            "FPSO BRAVO",
                            "Cálculos para TBMT",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8/elements"
                        },
                        new object[] {
                            polvoId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "3c92e53a-ea8b-11ed-b18d-0050568cb915",
                            "POLVO-A",
                            "Cálculos para Polvo",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE/elements"
                        },
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = (Guid)instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];

                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                    if (instanceId == bravoId)
                    {
                        var intakePressureId = Guid.NewGuid();
                        var pressurePDGId = Guid.NewGuid();
                        var WHPId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                intakePressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "94677f03-0080-11ee-b18f-0050568cb915",
                                "Intake Pressure ESP Sensor",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes"
                            },
                            new object[] {
                                pressurePDGId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE",
                                "7eedaab9-0080-11ee-b18f-0050568cb915",
                                "Pressure PDG 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE/attributes"
                            },
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "a8b5764a-0080-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ/attributes"
                            },
                        };


                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              bravoId
                              });


                            if (elementId == intakePressureId)
                            {

                                var osx3_66000053Id = Guid.NewGuid();
                                var osx3_66500053Id = Guid.NewGuid();
                                var osx3_66400053Id = Guid.NewGuid();
                                var osx3_66550053Id = Guid.NewGuid();
                                var osx3_66350053Id = Guid.NewGuid();
                                var osx3_66450053Id = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            osx3_66000053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQluqaGPBuAU-a-wE8Ix-IdwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY2MC0wMDUzLURBSUxZLUFWRw",
                                            "189aea96-6ef0-4f01-9afb-013c231f8877",
                                            "OSX3_PIT-6660-0053-DAILY-AVG",
                                            "Average ESP 6 Intake Pressure Well 9 - 10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQluqaGPBuAU-a-wE8Ix-IdwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY2MC0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQluqaGPBuAU-a-wE8Ix-IdwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY2MC0wMDUzLURBSUxZLUFWRw/value",
                                            "7-TBMT-10H-RJS"
                                        },
                                        new object[] {
                                            osx3_66500053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw",
                                            "fefcd4ca-431c-4449-adf8-9484a3eb0833",
                                            "OSX3_PIT-6650-0053-DAILY-AVG",
                                            "Average ESP 4 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw/value",
                                            "7-TBMT-8H-RJS"
                                        },
                                        new object[] {
                                            osx3_66400053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw",
                                            "87c9f2e6-fe8c-4559-8326-80bbc57e3339",
                                            "OSX3_PIT-6640-0053-DAILY-AVG",
                                            "Average ESP 2 Intake pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw/value",
                                            "9-OGX-44HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66550053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw",
                                            "ec618cd4-09bd-4871-96e9-908da6e7fd53",
                                            "OSX3_PIT-6655-0053-DAILY-AVG",
                                            "Average ESP 5 Intake Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw/value",
                                            "7-TBMT-4HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66350053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw",
                                            "359133b9-28b1-4d52-a627-ea15bb4ee137",
                                            "OSX3_PIT-6635-0053-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw/value",
                                            "7-TBMT-2HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66450053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw",
                                            "e1c76b56-91b8-4d75-8fbd-9a2b654706b3",
                                            "OSX3_PIT-6645-0053-DAILY-AVG",
                                            "Average ESP 3 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw/value",
                                            "7-TBMT-6HP-RJS"
                                        },
                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName
                                      });
                                }
                            }
                            else if (elementId == pressurePDGId)
                            {
                                var osx3_66350055Id = Guid.NewGuid();
                                var osx3_66400055Id = Guid.NewGuid();
                                var osx3_66450055Id = Guid.NewGuid();
                                var osx3_66500055Id = Guid.NewGuid();
                                var osx3_66550055Id = Guid.NewGuid();
                                var osx3_66000055Id = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            osx3_66350055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQCy0vflCw6EqHeGgTgQHZzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjYzNS0wMDU1LURBSUxZLUFWRw",
                                            "7e2f2d0b-b050-4ae8-8778-68138101d9cd",
                                            "OSX3_PIT-6635-0055-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQCy0vflCw6EqHeGgTgQHZzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjYzNS0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQCy0vflCw6EqHeGgTgQHZzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjYzNS0wMDU1LURBSUxZLUFWRw/value",
                                            "7-TBMT-2HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66400055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw",
                                            "eb82a42e-1ce5-4a80-918d-c042170f0a96",
                                            "OSX3_PIT-6640-0055-DAILY-AVG",
                                            "Average ESP 2 Downhole Pressure Well 5 - 44HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw/value",
                                            "9-OGX-44HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66450055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw",
                                            "9d26aa29-63a1-414d-b18f-ffea97ff079b",
                                            "OSX3_PIT-6645-0055-DAILY-AVG",
                                            "Average ESP 3 Downhole Pressure Well 6 - 6HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw/value",
                                            "7-TBMT-6HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66500055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw",
                                            "9aa56555-94ad-4180-a10f-b5cfdf43e2f0",
                                            "OSX3_PIT-6650-0055-DAILY-AVG",
                                            "Average ESP 4 Downhole Pressure Well 7 - 8H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw/value",
                                            "7-TBMT-8H-RJS"
                                        },
                                        new object[] {
                                            osx3_66550055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw",
                                            "765d0c75-ea0e-4d3c-a625-6552c4577f44",
                                            "OSX3_PIT-6655-0055-DAILY-AVG",
                                            "Average ESP 5 Downhole Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw/value",
                                            "7-TBMT-4HP-RJS"
                                        },
                                        new object[] {
                                            osx3_66000055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw",
                                            "8b13b755-feb6-4249-8373-4a09901f97a4",
                                            "OSX3_PIT-6660-0055-DAILY-AVG",
                                            "Average Downhole Pressure ESP 6 Well 9 - 10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw/value",
                                            "7-TBMT-10H-RJS"
                                        },
                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      pressurePDGId,
                                      attributeWellName
                                      });
                                }



                            }
                            else if (elementId == WHPId)
                            {
                                var osx3_106068Id = Guid.NewGuid();
                                var osx3_105668Id = Guid.NewGuid();
                                var osx3_105468Id = Guid.NewGuid();
                                var osx3_105568Id = Guid.NewGuid();
                                var osx3_105368Id = Guid.NewGuid();
                                var osx3_105268Id = Guid.NewGuid();


                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            osx3_106068Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "58edd297-0baf-4410-88e8-f188e34c73ec",
                                            "OSX3_PIT-1060-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 9 - TBMT-10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-10H-RJS"
                                        },
                                        new object[] {
                                            osx3_105668Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "33222c9c-1bed-4e33-9542-d4d7fa393a6d",
                                            "OSX3_PIT-1056-68-DAILY-AVG",
                                            "Average Pressão na ANM Well 7 - TBMT-08H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-8H-RJS"
                                        },
                                        new object[] {
                                            osx3_105468Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "055bdec5-20bd-4270-8770-cdf417514861",
                                            "OSX3_PIT-1054-68-DAILY-AVG",
                                            "Average Pressão ANM Well 5",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH/value",
                                            "9-OGX-44HP-RJS"
                                        },
                                        new object[] {
                                            osx3_105568Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "fca93775-1162-4a89-9fc0-0a7e001b64f4",
                                            "OSX3_PIT-1055-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 8 - TBMT-4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-4HP-RJS"
                                        },
                                        new object[] {
                                            osx3_105368Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "b7f47103-1c5f-4b6a-a3b1-856c3559aee2",
                                            "OSX3_PIT-1053-68-DAILY-AVG",
                                            "Average Pressão/Well 4",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-2HP-RJS"
                                        },
                                        new object[] {
                                            osx3_105268Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQRpLDWQKpDEyfy4jtPSy-zwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH",
                                            "59c39246-a902-4c0c-9fcb-88ed3d2cbecf",
                                            "OSX3_PIT-1052-68-DAILY-AVG",
                                            "Average Pressão ANM Well 6",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQRpLDWQKpDEyfy4jtPSy-zwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQRpLDWQKpDEyfy4jtPSy-zwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH/value",
                                            "7-TBMT-6HP-RJS"
                                        },

                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      WHPId,
                                      attributeWellName
                                      });
                                }

                            }

                        }
                    }
                    else if (instanceId == polvoId)
                    {
                        var intakePressureId = Guid.NewGuid();
                        var vibrationId = Guid.NewGuid();
                        var WHPId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                intakePressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "6674ade3-0084-11ee-b18f-0050568cb915",
                                "Intake Pressure ESP Sensor",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes"
                            },
                            new object[] {
                                vibrationId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                                "c1f96c32-509f-11ee-b198-0050568cb915",
                                "Vibração",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw/attributes"
                            },
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "6e648f8a-0084-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ/attributes"
                            },
                        };

                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              polvoId
                              });


                            if (elementId == intakePressureId)
                            {

                                var DH001AId = Guid.NewGuid();
                                var DH002AId = Guid.NewGuid();
                                var DH004AId = Guid.NewGuid();
                                var DH007AId = Guid.NewGuid();
                                var DH011AId = Guid.NewGuid();
                                var DH012AId = Guid.NewGuid();
                                var DH014AId = Guid.NewGuid();
                                var DH016AId = Guid.NewGuid();
                                var DH024AId = Guid.NewGuid();
                                var DH032AId = Guid.NewGuid();
                                var DH036AId = Guid.NewGuid();
                                var DH038AId = Guid.NewGuid();
                                var DH045AId = Guid.NewGuid();
                                var DH046AId = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            DH001AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQByFmFWJHOE259H5LOKMfOAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAxQS1EQUlMWS1BVkc",
                                            "15662107-4762-4d38-b9f4-7e4b38a31f38",
                                            "DH_PI_001A-DAILY-AVG",
                                            "Average QAY-001 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQByFmFWJHOE259H5LOKMfOAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAxQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQByFmFWJHOE259H5LOKMfOAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAxQS1EQUlMWS1BVkc/value",
                                            "POL-001-A"
                                        },
                                        new object[] {
                                            DH002AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc",
                                            "50101096-feaa-4e30-adf4-8038b5ec899b",
                                            "DH_PI_002A-DAILY-AVG",
                                            "Average QAY-002 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc/value",
                                            "POL-002-By"
                                        },
                                        new object[] {
                                            DH004AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc",
                                            "b53730d1-b181-40ba-b27e-be563a6ce3ef",
                                            "DH_PI_004A-DAILY-AVG",
                                            "Average QAY-004 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc/value",
                                            "POL-004-Cx"
                                        },
                                        new object[] {
                                            DH007AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc",
                                            "0d096b00-522b-47db-81c0-f46d5adeb508",
                                            "DH_PI_007A-DAILY-AVG",
                                            "Average QAY-007 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc/value",
                                            "POL-007-Gx"
                                        },
                                        new object[] {
                                            DH011AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc",
                                            "8a69634b-008a-4c89-a549-26fcd3e5bdc5",
                                            "DH_PI_011A-DAILY-AVG",
                                            "Average QAY-011 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc/value",
                                            "POL-011-Dy"
                                        },
                                        new object[] {
                                            DH012AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc",
                                            "c013028b-8c94-424f-8df5-be894b00e6b8",
                                            "DH_PI_012A-DAILY-AVG",
                                            "Average QAY-012 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc/value",
                                            "POL-012-R"
                                        },
                                        new object[] {
                                            DH014AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc",
                                            "47c22a3c-5fdb-48b1-868f-7c4fc32ccbce",
                                            "DH_PI_014A-DAILY-AVG",
                                            "Average QAY-014 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc/value",
                                            "POL-014-T"
                                        },
                                        new object[] {
                                            DH016AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc",
                                            "c9163e22-b7d2-4d9e-9fc0-6575ea65528e",
                                            "DH_PI_016A-DAILY-AVG",
                                            "Average QAY-016 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc/value",
                                            "POL-016-W"
                                        },
                                        new object[] {
                                            DH024AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc",
                                            "9ec2633a-ca26-4357-99c9-9a9fbde325c0",
                                            "DH_PI_024A-DAILY-AVG",
                                            "Average QAY-024 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc/value",
                                            "POL-024-Oy"
                                        },
                                        new object[] {
                                            DH032AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc",
                                            "18edec7a-80d1-4af6-be99-9ccdb533e2f8",
                                            "DH_PI_032A-DAILY-AVG",
                                            "Average QAY-032 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc/value",
                                            "POL-032-Xc"
                                        },
                                        new object[] {
                                            DH036AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc",
                                            "fb110617-8ec2-476e-baa5-57bd9723e07f",
                                            "DH_PI_036A-DAILY-AVG",
                                            "Average QAY-036 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc/value",
                                            "POL-036-Pj"
                                        },
                                        new object[] {
                                            DH038AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc",
                                            "dbda428e-675d-484b-8a27-c406f228a252",
                                            "DH_PI_038A-DAILY-AVG",
                                            "Average QAY-038 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc/value",
                                            "POL-038-Za"
                                        },
                                        new object[] {
                                            DH045AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc",
                                            "0e30aab1-2094-474f-930d-5624aefe4290",
                                            "DH_PI_045A-DAILY-AVG",
                                            "Average QAY-045 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc/value",
                                            "POL-045-L"
                                        },
                                        new object[] {
                                            DH046AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc",
                                            "21f8590c-0669-48a1-bd70-ee8b3d96455e",
                                            "DH_PI_046A-DAILY-AVG",
                                            "Average QAY-046 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc/value",
                                            "POL-046-K"
                                        },
                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName
                                      });
                                }
                            }
                            else if (elementId == WHPId)
                            {
                                var PXT001Id = Guid.NewGuid();
                                var PXT002Id = Guid.NewGuid();
                                var PXT004Id = Guid.NewGuid();
                                var PXT007Id = Guid.NewGuid();
                                var PXT011Id = Guid.NewGuid();
                                var PXT012Id = Guid.NewGuid();
                                var PXT014Id = Guid.NewGuid();
                                var PXT016Id = Guid.NewGuid();
                                var PXT024Id = Guid.NewGuid();
                                var PXT032Id = Guid.NewGuid();
                                var PXT036Id = Guid.NewGuid();
                                var PXT038Id = Guid.NewGuid();
                                var PXT045Id = Guid.NewGuid();
                                var PXT046Id = Guid.NewGuid();


                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            PXT001Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ1Q6-yDZlH0qO4zXISlWyogUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDEtREFJTFktQVZH",
                                            "c8be0ed5-6536-4a1f-8ee3-35c84a55b2a2",
                                            "PXT_001-DAILY-AVG",
                                            "Average Flowline FA1-001 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ1Q6-yDZlH0qO4zXISlWyogUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ1Q6-yDZlH0qO4zXISlWyogUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDEtREFJTFktQVZH/value",
                                            "POL-001-A"
                                        },
                                        new object[] {
                                            PXT002Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ3foUnhuJok2SU1agR07YwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDItREFJTFktQVZH",
                                            "9e14fadd-891b-4da2-9253-56a0474ed8c3",
                                            "PXT_002-DAILY-AVG",
                                            "Average Flowline FA1-002 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ3foUnhuJok2SU1agR07YwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ3foUnhuJok2SU1agR07YwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDItREFJTFktQVZH/value",
                                            "POL-002-By"
                                        },
                                        new object[] {
                                            PXT004Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQAsjXsg-VeECsE18sAEK6nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDQtREFJTFktQVZH",
                                            "b2d7c802-950f-4078-ac13-5f2c0042ba9f",
                                            "PXT_004-DAILY-AVG",
                                            "Average Flowline FA1-004 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQAsjXsg-VeECsE18sAEK6nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQAsjXsg-VeECsE18sAEK6nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDQtREFJTFktQVZH/value",
                                            "POL-004-Cx"
                                        },
                                        new object[] {
                                            PXT007Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQylc32iX_RkOPFp__IRf06wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDctREFJTFktQVZH",
                                            "da3757ca-ff25-4346-8f16-9fff2117f4eb",
                                            "PXT_007-DAILY-AVG",
                                            "Average Flowline FA1-007 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQylc32iX_RkOPFp__IRf06wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDctREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQylc32iX_RkOPFp__IRf06wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMDctREFJTFktQVZH/value",
                                            "POL-007-Gx"
                                        },
                                        new object[] {
                                            PXT011Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQdzwyUKMLtkuT7_jQDHCuRgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTEtREFJTFktQVZH",
                                            "50323c77-0ba3-4bb6-93ef-f8d00c70ae46",
                                            "PXT_011-DAILY-AVG",
                                            "Average Flowline FA1-011 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQdzwyUKMLtkuT7_jQDHCuRgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQdzwyUKMLtkuT7_jQDHCuRgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTEtREFJTFktQVZH/value",
                                            "POL-011-Dy"
                                        },
                                        new object[] {
                                            PXT012Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFKMRJBO1iUuNLlX8wsIrKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTItREFJTFktQVZH",
                                            "2411a314-b513-4b89-8d2e-55fcc2c22b2b",
                                            "PXT_012-DAILY-AVG",
                                            "Average Flowline FA1-012 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFKMRJBO1iUuNLlX8wsIrKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFKMRJBO1iUuNLlX8wsIrKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTItREFJTFktQVZH/value",
                                            "POL-012-R"
                                        },
                                        new object[] {
                                            PXT014Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQe4bjSwulpUWg9QQgLnoF6AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTQtREFJTFktQVZH",
                                            "4be3867b-a50b-45a5-a0f5-04202e7a05e8",
                                            "PXT_014-DAILY-AVG",
                                            "Average Flowline FA1-014 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQe4bjSwulpUWg9QQgLnoF6AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQe4bjSwulpUWg9QQgLnoF6AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTQtREFJTFktQVZH/value",
                                            "POL-014-T"
                                        },
                                        new object[] {
                                            PXT016Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQl8_Jna3jsEKOK9NH0q30HQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTYtREFJTFktQVZH",
                                            "9dc9cf97-e3ad-42b0-8e2b-d347d2adf41d",
                                            "PXT_016-DAILY-AVG",
                                            "Average Flowline FA1-016 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQl8_Jna3jsEKOK9NH0q30HQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQl8_Jna3jsEKOK9NH0q30HQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMTYtREFJTFktQVZH/value",
                                            "POL-016-W"
                                        },
                                        new object[] {
                                            PXT024Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFnY63Bt_5kWYrhEvE-h7agUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMjQtREFJTFktQVZH",
                                            "dc3a7616-7f1b-45e6-98ae-112f13e87b6a",
                                            "PXT_024-DAILY-AVG",
                                            "Average Flowline FA2-024 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFnY63Bt_5kWYrhEvE-h7agUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMjQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQFnY63Bt_5kWYrhEvE-h7agUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMjQtREFJTFktQVZH/value",
                                            "POL-024-Oy"
                                        },
                                        new object[] {
                                            PXT032Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQNcuN7m57z0GTt_4OypaVkQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzItREFJTFktQVZH",
                                            "ee8dcb35-7b6e-41cf-93b7-fe0eca969591",
                                            "PXT_032-DAILY-AVG",
                                            "Average Flowline FA1-032 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQNcuN7m57z0GTt_4OypaVkQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQNcuN7m57z0GTt_4OypaVkQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzItREFJTFktQVZH/value",
                                            "POL-032-Xc"
                                        },
                                        new object[] {
                                            PXT036Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQHpuGBjISyU2HpIIPCREdlQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzYtREFJTFktQVZH",
                                            "06869b1e-1232-4dc9-87a4-820f09111d95",
                                            "PXT_036-DAILY-AVG",
                                            "Average Flowline FA1-036 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQHpuGBjISyU2HpIIPCREdlQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQHpuGBjISyU2HpIIPCREdlQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzYtREFJTFktQVZH/value",
                                            "POL-036-Pj"
                                        },
                                        new object[] {
                                            PXT038Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQloQOkdCIVUi35r7u-5LzpgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzgtREFJTFktQVZH",
                                            "910e8496-88d0-4855-b7e6-beeefb92f3a6",
                                            "PXT_038-DAILY-AVG",
                                            "Average Flowline FA1-038 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQloQOkdCIVUi35r7u-5LzpgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQloQOkdCIVUi35r7u-5LzpgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wMzgtREFJTFktQVZH/value",
                                            "POL-038-Za"
                                        },
                                        new object[] {
                                            PXT045Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQLHaGSuYKpUidoWZa-SFSyAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDUtREFJTFktQVZH",
                                            "4a86762c-0ae6-48a5-9da1-665af92152c8",
                                            "PXT_045-DAILY-AVG",
                                            "Average Flowline FA1-045 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQLHaGSuYKpUidoWZa-SFSyAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDUtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQLHaGSuYKpUidoWZa-SFSyAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDUtREFJTFktQVZH/value",
                                            "POL-045-L"
                                        },
                                        new object[] {
                                            PXT046Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ4ny166jg8U2QonjskdxfewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDYtREFJTFktQVZH",
                                            "ebb57ce2-e0a8-4df1-90a2-78ec91dc5f7b",
                                            "PXT_046-DAILY-AVG",
                                            "Average Flowline FA1-046 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ4ny166jg8U2QonjskdxfewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQ4ny166jg8U2QonjskdxfewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQfFBYVF8wNDYtREFJTFktQVZH/value",
                                            "POL-046-K"
                                        },

                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName
                                      });
                                }
                            }


                        }
                    }

                }

            }
            else if (instanceKey.ToUpper() == "FORTE")
            {

                var forteId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            forteId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "5550d370-ea8b-11ed-b18d-0050568cb915",
                            "FPSO FORTE",
                            "Cálculos para Albacora",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU/elements"
                        }
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = (Guid)instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];

                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                    if (instanceId == forteId)
                    {
                        var PDGPressureId = Guid.NewGuid();
                        var TPTPressureId = Guid.NewGuid();
                        var GASLiftId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                PDGPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "7247ad3d-27ec-11ee-b194-0050568cc6e1",
                                "PDG Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE/attributes"
                            },
                            new object[] {
                                TPTPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "a1158244-27ef-11ee-b194-0050568cc6e1",
                                "TPT Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE/attributes"
                            },
                            new object[] {
                                GASLiftId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "689796c7-27ff-11ee-b194-0050568cc6e1",
                                "Vazão de gás lift",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA/attributes"
                            },
                        };

                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              forteId
                              });

                            if (elementId == PDGPressureId)
                            {

                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q3i9V2xh_pEmiOhMCXNr9zAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNSLURBSUxZLUFWRw",
                                            "db552fde-7f18-49a4-a23a-13025cdafdcc",
                                            "P50_PT-1210023R-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q3i9V2xh_pEmiOhMCXNr9zAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNSLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q3i9V2xh_pEmiOhMCXNr9zAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNSLURBSUxZLUFWRw/value",
                                            "ABL-16HP"
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw",
                                            "ef217ee7-ca3b-46c7-9438-5cb122ea63dd",
                                            "P50_PT-1210023N-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw/value",
                                            "ABL-24HP"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw",
                                            "f06801c4-bdd7-4ae1-89a7-455930913a17",
                                            "P50_PT-1210023B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw/value",
                                            "ABL-87HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "cd8fd6a3-0bed-4b2e-bcbf-6f866d074a27",
                                            "P50_PT-1210023A-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw/value",
                                            "ABL-81HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QXdQhiWoSEUeedQbR8Ub06QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNTLURBSUxZLUFWRw",
                                            "8921d45d-126a-4711-9e75-06d1f146f4e9",
                                            "P50_PT-1210023S-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw/value",
                                            "ABL-84HP"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw",
                                            "3522d01d-e395-4d3e-85bc-bc291311c399",
                                            "P50_PT-1210023D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw/value",
                                            "AB-134HPA"
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QlhqqSSlKZkSg3-h6bbzXZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNMLURBSUxZLUFWRw",
                                            "49aa1a96-4a29-4466-a0df-e87a6dbcd767",
                                            "P50_PT-1210023L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QlhqqSSlKZkSg3-h6bbzXZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNMLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QlhqqSSlKZkSg3-h6bbzXZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNMLURBSUxZLUFWRw/value",
                                            "ABL-54HP"
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QaxxZkhM_k0O0bVqgT-uqVAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNNLURBSUxZLUFWRw",
                                            "92591c6b-3f13-4393-b46d-5aa04febaa54",
                                            "P50_PT-1210023M-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QaxxZkhM_k0O0bVqgT-uqVAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNNLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QaxxZkhM_k0O0bVqgT-uqVAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNNLURBSUxZLUFWRw/value",
                                            "ABL-13HP"
                                        },
                                };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDGPressureId,
                                      attributeWellName
                                      });
                                }
                            }

                            else if (elementId == TPTPressureId)
                            {
                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qgx3m_tWMTEqz5mw5k7RMEgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJMLURBSUxZLUFWRw",
                                            "P50_PT-1210012L-DAILY-AVG",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qgx3m_tWMTEqz5mw5k7RMEgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJMLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qgx3m_tWMTEqz5mw5k7RMEgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJMLURBSUxZLUFWRw/value",
                                            "ABL-54HP"
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw",
                                            "P50_PT-1210012M-DAILY-AVG",
                                            "3b59b446-80c4-4951-9b7f-b96eed0bba7a",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw/value",
                                            "ABL-13HP"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw",
                                            "P50_PT-1210012R-DAILY-AVG",
                                            "167f61c5-73f7-4155-ad69-b8558580dbb8",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw/value",
                                            "ABL-16HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw",
                                            "P50_PT-1210012N-DAILY-AVG",
                                            "715acdcb-bf12-47c5-82ea-104c0db2bf72",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw/value",
                                            "ABL-24HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw",
                                            "P50_PT-1210012B-DAILY-AVG",
                                            "69f5f7db-ea9b-4b75-9624-70ff2c400b53",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw/value",
                                            "ABL-87HP"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw",
                                            "P50_PT-1210012A-DAILY-AVG",
                                            "64fd2eeb-b2d3-4023-a67d-4c551148d324",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw/value",
                                            "ABL-81HP"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw",
                                            "P50_PT-1210012S-DAILY-AVG",
                                            "48aab444-fb25-49ee-af97-6655e747df07",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw/value",
                                            "ABL-84HP"
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw",
                                            "P50_PT-1210012D-DAILY-AVG",
                                            "4b42a5af-b636-4ab3-b43d-b5a4a1dbfd79",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw/value",
                                            "AB-134HPA"
                                        },
                                };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      TPTPressureId,
                                      attributeWellName
                                      });
                                }

                            }

                            else if (elementId == GASLiftId)
                            {
                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q-PSR00Vp6UmpKBaZtT24ngUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH",
                                            "d391f4f8-6945-49e9-a928-1699b53db89e",
                                            "P50_FT-1231010L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q-PSR00Vp6UmpKBaZtT24ngUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q-PSR00Vp6UmpKBaZtT24ngUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH/value",
                                            "ABL-54HP"
                                        },

                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "bb2b48b1-9d04-48bf-92bf-2f8688dfadf6",
                                            "P50_FT-1231010M-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH/value",
                                            "ABL-13HP"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "679b3e8c-b379-4336-8ba1-4b29d6ab4bf4",
                                            "P50_FT-1231010R-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH/value",
                                            "ABL-16HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "d04402d6-1da3-4c8f-ad1e-c75e5c7cf43c",
                                            "P50_FT-1231010N-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH/value",
                                            "ABL-24HP"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "8a8d2d21-96e6-463f-8f4a-5e8453721dab",
                                            "P50_FT-1231010B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH/value",
                                            "ABL-87HP"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "2180c9e7-299d-41af-9156-10d4c659d75f",
                                            "P50_FT-1231010A-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH/value"
                                            ,"ABL-81HP"
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "de984ba3-c1fe-4563-afbe-c6f9b9b6d976",
                                            "P50_FT-1231010S-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH/value",
                                            "ABL-84HP"
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "b5d9768f-164a-45d8-a3ce-baea78c024c3",
                                            "P50_FT-1231010D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH/value",
                                            "AB-134HPA"
                                        },
                                };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      GASLiftId,
                                      attributeWellName
                                      });
                                }

                            }
                        }
                    }
                }

            }
            else if (instanceKey.ToUpper() == "VALENTE")
            {
                var fradeId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            fradeId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "4b541470-ea8b-11ed-b18d-0050568cb915",
                            "FPSO FRADE",
                            "Cálculos para Frade",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcBRUS4vq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREU",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcBRUS4vq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREU/elements"
                        }
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = (Guid)instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];


                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                    if (instanceId == fradeId)
                    {
                        var PDG1Id = Guid.NewGuid();
                        var PDG2Id = Guid.NewGuid();
                        var SSPCVId = Guid.NewGuid();
                        var VConeId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                PDG1Id,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx",
                                "02126f19-2589-11ee-b197-0050568cf50d",
                                "Pressao de Fundo de Poço 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx/attributes"
                            },
                            new object[] {
                                PDG2Id,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy",
                                "0e21dcb6-2589-11ee-b197-0050568cf50d",
                                "Pressao de Fundo de Poço 2",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy/attributes"
                            },
                            new object[] {
                                SSPCVId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg",
                                "18602e1e-2589-11ee-b197-0050568cf50d",
                                "Pressao Montante SSPCV",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg/attributes"
                            },
                            new object[] {
                                VConeId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "44761040-2589-11ee-b197-0050568cf50d",
                                "Vazao VCone",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes"
                            },
                        };

                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              fradeId
                              });

                            if (elementId == PDG1Id)
                            {

                                var ODP4086 = Guid.NewGuid();
                                var MUP5086 = Guid.NewGuid();
                                var MDP2086 = Guid.NewGuid();
                                var ODP3086 = Guid.NewGuid();
                                var MUP2086 = Guid.NewGuid();
                                var N5P1086 = Guid.NewGuid();
                                var OUP2086 = Guid.NewGuid();
                                var ODP5086 = Guid.NewGuid();
                                var MDP1086 = Guid.NewGuid();
                                var MUP4086 = Guid.NewGuid();
                                var N5P2086 = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            ODP4086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQqHmqSw65REWiN83S4fmd-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "4baa79a8-b90e-4544-a237-cdd2e1f99dfb",
                                            "FRSS-PI-ODP4-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQqHmqSw65REWiN83S4fmd-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQqHmqSw65REWiN83S4fmd-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP4"
                                        },
                                        new object[] {
                                            MUP5086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ2Cvw20dcHUmASqskEvOrHQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "dbf02bd8-5c47-491d-804a-ab2412f3ab1d",
                                            "FRSS-PI-MUP5-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ2Cvw20dcHUmASqskEvOrHQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ2Cvw20dcHUmASqskEvOrHQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP5"
                                        },
                                        new object[] {
                                            MDP2086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQoLQdBSU6-kSq74Kn-dCnpwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "051db4a0-3a25-44fa-aaef-82a7f9d0a7a7",
                                            "FRSS-PI-MDP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQoLQdBSU6-kSq74Kn-dCnpwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQoLQdBSU6-kSq74Kn-dCnpwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP2"
                                        },
                                        new object[] {
                                            ODP3086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ-WVgSxbcfkSX95hoHH29XwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "4b6065f9-dc16-447e-97f7-98681c7dbd5f",
                                            "FRSS-PI-ODP3-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ-WVgSxbcfkSX95hoHH29XwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ-WVgSxbcfkSX95hoHH29XwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP3"
                                        },
                                        new object[] {
                                            MUP2086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ24JCAGKaWUGoFW42ycdwfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "004282db-9a62-4159-a815-6e36c9c7707f",
                                            "FRSS-PI-MUP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ24JCAGKaWUGoFW42ycdwfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQ24JCAGKaWUGoFW42ycdwfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP2"
                                        },
                                        new object[] {
                                            N5P1086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQTwq_YP-xQk6zvjakdPp-fAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "60bf0a4f-b1ff-4e42-b3be-36a474fa7e7c",
                                            "FRSS-PI-N5P1-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQTwq_YP-xQk6zvjakdPp-fAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQTwq_YP-xQk6zvjakdPp-fAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P1"
                                        },
                                        new object[] {
                                            OUP2086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQIfy_16pCTUC-ZF0jTXO8RgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "d7bffc21-42aa-404d-be64-5d234d73bc46",
                                            "FRSS-PI-OUP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQIfy_16pCTUC-ZF0jTXO8RgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQIfy_16pCTUC-ZF0jTXO8RgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "OUP2"
                                        },
                                        new object[] {
                                            ODP5086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQltOOjFkbkkeK-nDFq7UfPgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "8c8ed396-1b59-4792-8afa-70c5abb51f3e",
                                            "FRSS-PI-ODP5-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQltOOjFkbkkeK-nDFq7UfPgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQltOOjFkbkkeK-nDFq7UfPgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP5"
                                        },
                                        new object[] {
                                            MDP1086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQwWdjIuZV-UagTsf3Rho7LQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "226367c1-55e6-46f9-a04e-c7f7461a3b2d",
                                            "FRSS-PI-MDP1-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQwWdjIuZV-UagTsf3Rho7LQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQwWdjIuZV-UagTsf3Rho7LQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP1"
                                        },
                                        new object[] {
                                            MUP4086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQxsJrnqovXE2wCb_UJb2aKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "9e6bc2c6-2faa-4d5c-b009-bfd425bd9a2b",
                                            "FRSS-PI-MUP4-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQxsJrnqovXE2wCb_UJb2aKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQxsJrnqovXE2wCb_UJb2aKwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP4"
                                        },
                                        new object[] {
                                            N5P2086,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQAaH9A3vW20uc3gTysCod-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "03fda101-d67b-4bdb-9cde-04f2b02a1dfb",
                                            "FRSS-PI-N5P2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQAaH9A3vW20uc3gTysCod-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQAaH9A3vW20uc3gTysCod-wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAxfEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P2"
                                        },
                                    };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDG1Id,
                                      attributeWellName
                                      });
                                }
                            }
                            else if (elementId == PDG2Id)
                            {
                                var ODP4096 = Guid.NewGuid();
                                var MUP5096 = Guid.NewGuid();
                                var MDP2096 = Guid.NewGuid();
                                var ODP3096 = Guid.NewGuid();
                                var MUP2096 = Guid.NewGuid();
                                var N5P1096 = Guid.NewGuid();
                                var OUP2096 = Guid.NewGuid();
                                var ODP5096 = Guid.NewGuid();
                                var MDP1096 = Guid.NewGuid();
                                var MUP4096 = Guid.NewGuid();
                                var N5P2096 = Guid.NewGuid();
                                var MUP3A096 = Guid.NewGuid();
                                var N5I1096 = Guid.NewGuid();
                                var OUP1096 = Guid.NewGuid();
                                var ODI1A096 = Guid.NewGuid();
                                var ODI2096 = Guid.NewGuid();
                                var ODP1096 = Guid.NewGuid();
                                var OUP3096 = Guid.NewGuid();
                                var OUI2096 = Guid.NewGuid();
                                var OUI3096 = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            ODP4096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQhS4c4rF-Rk6M2jccm2q5jQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "e21c2e85-7eb1-4e46-8cda-371c9b6ab98d",
                                            "FRSS-PI-ODP4-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQhS4c4rF-Rk6M2jccm2q5jQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQhS4c4rF-Rk6M2jccm2q5jQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP4"
                                        },
                                        new object[] {
                                            MUP5096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQr1Zxvz-s90-LslN36J4BSwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "bf7156af-ac3f-4ff7-8bb2-5377e89e014b",
                                            "FRSS-PI-MUP5-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQr1Zxvz-s90-LslN36J4BSwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQr1Zxvz-s90-LslN36J4BSwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP5"
                                        },
                                        new object[] {
                                            MDP2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPZEiwxxZMUmqcHYwLSpG7QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "c322913d-591c-4931-aa70-76302d2a46ed",
                                            "FRSS-PI-MDP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPZEiwxxZMUmqcHYwLSpG7QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPZEiwxxZMUmqcHYwLSpG7QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP2"
                                        },
                                        new object[] {
                                            ODP3096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQDQvWWhot3EOss7tN-usmWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "ad60b0d-2d1a-43dc-acb3-bb4dfaeb265b",
                                            "FRSS-PI-ODP3-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQDQvWWhot3EOss7tN-usmWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQDQvWWhot3EOss7tN-usmWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP3"
                                        },
                                        new object[] {
                                            MUP2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ34oky-88_k636j4sLcEeigUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "cb248adf-3cef-4efe-b7ea-3e2c2dc11e8a",
                                            "FRSS-PI-MUP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ34oky-88_k636j4sLcEeigUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ34oky-88_k636j4sLcEeigUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQMi0wOTYtS0dGLURBSUxZLUFWRw/value"
                                            ,"MUP2"
                                        },
                                        new object[] {
                                            N5P1096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ8BYBLA3Y5EiqkRk3zMh8AgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "2c0116f0-d80d-48e4-aa91-1937ccc87c02",
                                            "FRSS-PI-N5P1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ8BYBLA3Y5EiqkRk3zMh8AgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ8BYBLA3Y5EiqkRk3zMh8AgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P1"
                                        },
                                        new object[] {
                                            OUP2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQSpZFQirpJ0qLFFPN8XhpHAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "4245964a-e92a-4a27-8b14-53cdf178691c",
                                            "FRSS-PI-OUP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQSpZFQirpJ0qLFFPN8XhpHAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQSpZFQirpJ0qLFFPN8XhpHAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUP2"
                                        },
                                        new object[] {
                                            ODP5096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQgU54nnm0mk23zITAzjieHgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "9e784e81-b479-4d9a-b7cc-84c0ce389e1e",
                                            "FRSS-PI-ODP5-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQgU54nnm0mk23zITAzjieHgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQgU54nnm0mk23zITAzjieHgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP5"
                                        },
                                        new object[] {
                                            MDP1096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQtW71vwzqiUunR9-ttnDWIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "bff56eb5-ea0c-4b89-a747-dfadb670d621",
                                            "FRSS-PI-MDP1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQtW71vwzqiUunR9-ttnDWIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQtW71vwzqiUunR9-ttnDWIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP1"
                                        },
                                        new object[] {
                                            MUP4096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQRNrLpG8O2EqD08FUjG9QUQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "a4cbda44-0e6f-4ad8-83d3-c1548c6f5051",
                                            "FRSS-PI-MUP4-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQRNrLpG8O2EqD08FUjG9QUQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQRNrLpG8O2EqD08FUjG9QUQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP4"
                                        },
                                        new object[] {
                                            N5P2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQZQKJkNp3JkahpC0q4msG1QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "90890265-77da-4626-a1a4-2d2ae26b06d5",
                                            "FRSS-PI-N5P2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQZQKJkNp3JkahpC0q4msG1QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQZQKJkNp3JkahpC0q4msG1QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P2"
                                        },
                                        new object[] {
                                            MUP3A096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQViYOgHVhqEKm27c24szmZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQM0EtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "800e2656-6175-42a8-a6db-b736e2cce667",
                                            "FRSS-PI-MUP3A-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQViYOgHVhqEKm27c24szmZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQM0EtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQViYOgHVhqEKm27c24szmZwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTVVQM0EtMDk2LUtHRi1EQUlMWS1BVkc/value",
                                            "MUP3A"
                                        },
                                        new object[] {
                                            N5I1096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPmXWekzGTkC6NYJ9Xm34DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "7ad6653e-c64c-404e-ba35-827d5e6df80d",
                                            "FRSS-PI-N5I1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPmXWekzGTkC6NYJ9Xm34DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQPmXWekzGTkC6NYJ9Xm34DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5I1"
                                        },
                                        new object[] {
                                            OUP1096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQren8lfxdckCQr-kV6oB0MgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "95fce9ad-5dfc-4072-90af-e915ea807432",
                                            "FRSS-PI-OUP1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQren8lfxdckCQr-kV6oB0MgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQren8lfxdckCQr-kV6oB0MgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMS0wOTYtS0dGLURBSUxZLUFWRw/valuee",
                                            "OUP1"
                                        },
                                        new object[] {
                                            ODI1A096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQkrT1EDl0Zkq9Y8nh2CgbVgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "10f5b492-7439-4a66-bd63-c9e1d8281b56",
                                            "FRSS-PI-ODI1A-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQkrT1EDl0Zkq9Y8nh2CgbVgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQkrT1EDl0Zkq9Y8nh2CgbVgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc/value",
                                            "ODI1A"
                                        },
                                        new object[] {
                                            ODI2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ-lZdRsRTUkCZZpmjInVivQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "465d56fa-53c4-4052-9966-99a3227562bd",
                                            "FRSS-PI-ODI2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ-lZdRsRTUkCZZpmjInVivQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ-lZdRsRTUkCZZpmjInVivQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODI2"
                                        },
                                        new object[] {
                                            ODP1096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ1uEgopzHQ0ukP4kD2QxWrwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "a220e1d6-c79c-4b43-a43f-8903d90c56af",
                                            "FRSS-PI-ODP1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ1uEgopzHQ0ukP4kD2QxWrwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ1uEgopzHQ0ukP4kD2QxWrwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT0RQMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP1"
                                        },
                                        new object[] {
                                            OUP3096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQWh4ZBCJzBU-0kw4GjH1vZAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "04191e5a-7322-4f05-b493-0e068c7d6f64",
                                            "FRSS-PI-OUP3-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQWh4ZBCJzBU-0kw4GjH1vZAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQWh4ZBCJzBU-0kw4GjH1vZAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VQMy0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUP3"
                                        },
                                        new object[] {
                                            OUI2096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ9fJvMdC10EilmNzJn5FAIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "316ff2f5-b5d0-48d0-a598-dcc99f914021",
                                            "FRSS-PI-OUI2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ9fJvMdC10EilmNzJn5FAIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQ9fJvMdC10EilmNzJn5FAIQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUI2"
                                        },
                                        new object[] {
                                            OUI3096,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQeCWnYw3RokaiZHMzc4oKTgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "63a72578-d10d-46a2-a264-7333738a0a4e",
                                            "FRSS-PI-OUI3-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQeCWnYw3RokaiZHMzc4oKTgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQeCWnYw3RokaiZHMzc4oKTgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAyfEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUI3"
                                        },
                                    };
                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDG2Id,
                                      attributeWellName
                                      });
                                }

                            }
                            else if (elementId == VConeId)
                            {

                                var ODP4153 = Guid.NewGuid();
                                var MUP5153 = Guid.NewGuid();
                                var MDP2153 = Guid.NewGuid();
                                var ODP3153 = Guid.NewGuid();
                                var MUP2153 = Guid.NewGuid();
                                var N5P1153 = Guid.NewGuid();
                                var OUP2153 = Guid.NewGuid();
                                var ODP5153 = Guid.NewGuid();
                                var MDP1153 = Guid.NewGuid();
                                var MUP4153 = Guid.NewGuid();
                                var N5P2153 = Guid.NewGuid();


                                var attributesData = new List<object[]>
                                {
                                    new object[] {
                                        ODP4153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQL_C7V7JWtk2ajqrO8lSFrAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA0LTE1My1EQUlMWS1BVkc",
                                        "57bbf02f-56b2-4db6-9a8e-aacef25485ac",
                                        "FRSS-FI-ODP4-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQL_C7V7JWtk2ajqrO8lSFrAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA0LTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQL_C7V7JWtk2ajqrO8lSFrAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA0LTE1My1EQUlMWS1BVkc/value",
                                        "ODP4"
                                    },
                                    new object[] {
                                        MUP5153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQZ1E_8p922kKdBBFXm5cVWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA1LTE1My1EQUlMWS1BVkc",
                                        "f23f5167-769f-42da-9d04-11579b97155b",
                                        "FRSS-FI-MUP5-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQZ1E_8p922kKdBBFXm5cVWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA1LTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQZ1E_8p922kKdBBFXm5cVWwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA1LTE1My1EQUlMWS1BVkc/value",
                                        "MUP5"
                                    },
                                    new object[] {
                                        MDP2153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQbYeNVbmkuEuMMRhfk93C8gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAyLTE1My1EQUlMWS1BVkc",
                                        "558d876d-a4b9-4bb8-8c31-185f93ddc2f2",
                                        "FRSS-FI-MDP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQbYeNVbmkuEuMMRhfk93C8gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAyLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQbYeNVbmkuEuMMRhfk93C8gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAyLTE1My1EQUlMWS1BVkc/value",
                                        "MDP2"
                                    },
                                    new object[] {
                                        ODP3153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQHjlVY_RbP064pi_YhJg3RQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFAzLTE1My1EQUlMWS1BVkc",
                                        "6355391e-5bf4-4e3f-b8a6-2fd884983745",
                                        "FRSS-FI-ODP3-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQHjlVY_RbP064pi_YhJg3RQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFAzLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQHjlVY_RbP064pi_YhJg3RQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFAzLTE1My1EQUlMWS1BVkc/value",
                                        "ODP3"
                                    },
                                    new object[] {
                                        MUP2153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPwnLfXVYvUSezN9ytbjmYAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVAyLTE1My1EQUlMWS1BVkc",
                                        "7dcb093f-5875-44bd-9ecc-df72b5b8e660",
                                        "FRSS-FI-MUP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPwnLfXVYvUSezN9ytbjmYAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVAyLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPwnLfXVYvUSezN9ytbjmYAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVAyLTE1My1EQUlMWS1BVkc/value",
                                        "MUP2"
                                    },
                                    new object[] {
                                        N5P1153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ_dM66wqxC02MJ6f64aonagUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAxLTE1My1EQUlMWS1BVkc",
                                        "eb3ad3fd-b10a-4d0b-8c27-a7fae1aa276a",
                                        "FRSS-FI-N5P1-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ_dM66wqxC02MJ6f64aonagUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAxLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ_dM66wqxC02MJ6f64aonagUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAxLTE1My1EQUlMWS1BVkc/value",
                                        "N5P1"
                                    },
                                    new object[] {
                                        OUP2153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQOuFwtNC5vUqoB7JSw26ZKAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PVVAyLTE1My1EQUlMWS1BVkc",
                                        "b470e13a-b9d0-4abd-a807-b252c36e9928",
                                        "FRSS-FI-OUP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQOuFwtNC5vUqoB7JSw26ZKAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PVVAyLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQOuFwtNC5vUqoB7JSw26ZKAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PVVAyLTE1My1EQUlMWS1BVkc/value",
                                        "OUP2"
                                    },
                                    new object[] {
                                        ODP5153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUB08k6uT_kSKA-pRlTYvugUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA1LTE1My1EQUlMWS1BVkc",
                                        "933c1d50-93ab-44fe-8a03-ea5195362fba",
                                        "FRSS-FI-ODP5-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUB08k6uT_kSKA-pRlTYvugUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA1LTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUB08k6uT_kSKA-pRlTYvugUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1PRFA1LTE1My1EQUlMWS1BVkc/value",
                                        "ODP5"
                                    },
                                    new object[] {
                                        MDP1153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPCsXeVCBlU6biGKLTvTm9QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAxLTE1My1EQUlMWS1BVkc",
                                        "79172b3c-8150-4e95-9b88-628b4ef4e6f5",
                                        "FRSS-FI-MDP1-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPCsXeVCBlU6biGKLTvTm9QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAxLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQPCsXeVCBlU6biGKLTvTm9QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NRFAxLTE1My1EQUlMWS1BVkc/value",
                                        "MDP1"
                                    },
                                    new object[] {
                                        MUP4153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ3W-cay1zcUeTI6uZdsdXkwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA0LTE1My1EQUlMWS1BVkc",
                                        "6b9c6fdd-732d-4771-9323-ab9976c75793",
                                        "FRSS-FI-MUP4-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ3W-cay1zcUeTI6uZdsdXkwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA0LTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQ3W-cay1zcUeTI6uZdsdXkwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1NVVA0LTE1My1EQUlMWS1BVkc/value",
                                        "MUP4"
                                    },
                                    new object[] {
                                        N5P2153,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQzoHCMb3EqEqvnup0zlAvswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAyLTE1My1EQUlMWS1BVkc",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-N5P2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQzoHCMb3EqEqvnup0zlAvswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAyLTE1My1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQzoHCMb3EqEqvnup0zlAvswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkV8RlJTUy1GSS1ONVAyLTE1My1EQUlMWS1BVkc/value",
                                        "N5P2"
                                    },
                                };

                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      VConeId,
                                      attributeWellName
                                      });
                                }


                            }
                            else if (elementId == SSPCVId)
                            {
                                var ODP4043 = Guid.NewGuid();
                                var MUP5043 = Guid.NewGuid();
                                var MDP2043 = Guid.NewGuid();
                                var ODP3043 = Guid.NewGuid();
                                var MUP2043 = Guid.NewGuid();
                                var N5P1043 = Guid.NewGuid();
                                var OUP2043 = Guid.NewGuid();
                                var ODP5043 = Guid.NewGuid();
                                var MDP1043 = Guid.NewGuid();
                                var MUP4043 = Guid.NewGuid();
                                var N5P2043 = Guid.NewGuid();

                                var attributesData = new List<object[]>
                                {
                                    new object[] {
                                        ODP4043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQlslO-26ssEOPrB6WqfLJzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDQtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "fb4ec996-ac6e-43b0-8fac-1e96a9f2c9cd",
                                        "FRSS-PI-ODP4-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQlslO-26ssEOPrB6WqfLJzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDQtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQlslO-26ssEOPrB6WqfLJzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDQtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "ODP4"
                                    },
                                    new object[] {
                                        MUP5043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQCkDHDvZ2SU2c5LK8_RZiFQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDUtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "0ec7400a-76f6-4d49-9ce4-b2bcfd166215",
                                        "FRSS-PI-MUP5-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQCkDHDvZ2SU2c5LK8_RZiFQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDUtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQCkDHDvZ2SU2c5LK8_RZiFQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDUtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "MUP5"
                                    },
                                    new object[] {
                                        MDP2043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQAg_RjKvuz0SA9hFx8X4A5AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "8cd10f02-eeab-44cf-80f6-1171f17e00e4",
                                        "FRSS-PI-MDP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQAg_RjKvuz0SA9hFx8X4A5AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQAg_RjKvuz0SA9hFx8X4A5AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDItMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "MDP2"
                                    },
                                    new object[] {
                                        ODP3043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQK8OKu0FAR0KtiVPgCCn97gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDMtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "bb8ac32b-4041-4247-ad89-53e00829fdee",
                                        "FRSS-PI-ODP3-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQK8OKu0FAR0KtiVPgCCn97gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDMtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQK8OKu0FAR0KtiVPgCCn97gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDMtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "ODP3"
                                    },
                                    new object[] {
                                        MUP2043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQ7gFCEtxzlUGB1fgPknQvMAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "124201ee-73dc-4195-81d5-f80f92742f30",
                                        "FRSS-PI-MUP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQ7gFCEtxzlUGB1fgPknQvMAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQ7gFCEtxzlUGB1fgPknQvMAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDItMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "MUP2"
                                    },
                                    new object[] {
                                        N5P1043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQb5t3UtgBi0eeACIiLYF7CgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "52779b6f-01d8-478b-9e00-22222d817b0a",
                                        "FRSS-PI-N5P1-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQb5t3UtgBi0eeACIiLYF7CgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQb5t3UtgBi0eeACIiLYF7CgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDEtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "N5P1"
                                    },
                                    new object[] {
                                        OUP2043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQeLVw-ij170S3VcPQaeUelgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9VUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "fa70b578-f528-44ef-b755-c3d069e51e96",
                                        "FRSS-PI-OUP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQeLVw-ij170S3VcPQaeUelgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9VUDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQeLVw-ij170S3VcPQaeUelgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9VUDItMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "OUP2"
                                    },
                                    new object[] {
                                        ODP5043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQfa9VoiZgw0SrqvrefXg3egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDUtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "a255af7d-6026-44c3-abaa-fade7d78377a",
                                        "FRSS-PI-ODP5-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQfa9VoiZgw0SrqvrefXg3egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDUtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQfa9VoiZgw0SrqvrefXg3egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU9EUDUtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "ODP5"
                                    },
                                    new object[] {
                                        MDP1043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQsQpQvDNU80aSqeJ_T_l2ewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "bc500ab1-5433-46f3-92a9-e27f4ff9767b",
                                        "FRSS-PI-MDP1-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQsQpQvDNU80aSqeJ_T_l2ewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQsQpQvDNU80aSqeJ_T_l2ewUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1EUDEtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "MDP1"
                                    },
                                    new object[] {
                                        MUP4043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQTjFXLpcMJEebgFNOTld-nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDQtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "2e57314e-0c97-4724-9b80-534e4e577e9f",
                                        "FRSS-PI-MUP4-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQTjFXLpcMJEebgFNOTld-nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDQtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQTjFXLpcMJEebgFNOTld-nwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU1VUDQtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "MUP4"
                                    },
                                    new object[] {
                                        N5P2043,
                                        "F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQdv__iXTF4UyJyHncFItypgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "89ffff76-c574-4ce1-89c8-79dc148b72a6",
                                        "FRSS-PI-N5P2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQdv__iXTF4UyJyHncFItypgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDItMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQdv__iXTF4UyJyHncFItypgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVnxGUlNTLVBJLU41UDItMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "N5P2"
                                    },

                                };
                                foreach (var attribute in attributesData)
                                {
                                    var attributeId = (Guid)attribute[0];
                                    var attributeWebId = attribute[1];
                                    var attributePIId = attribute[2];
                                    var attributeName = attribute[3];
                                    var attributeDescription = attribute[4];
                                    var attributeSelfRoute = attribute[5];
                                    var attributeElementsRoute = attribute[6];
                                    var attributeWellName = attribute[7];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName
                                      });
                                }
                            }
                        }

                    }

                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
