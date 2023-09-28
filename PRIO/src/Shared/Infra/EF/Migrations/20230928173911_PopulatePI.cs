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

            Console.WriteLine(instanceKey);

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
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQluqaGPBuAU-a-wE8Ix-IdwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY2MC0wMDUzLURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66500053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw",
                                            "fefcd4ca-431c-4449-adf8-9484a3eb0833",
                                            "OSX3_PIT-6650-0053-DAILY-AVG",
                                            "Average ESP 4 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQytT8_hxDSUSt-JSEo-sIMwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1MC0wMDUzLURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66400053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw",
                                            "87c9f2e6-fe8c-4559-8326-80bbc57e3339",
                                            "OSX3_PIT-6640-0053-DAILY-AVG",
                                            "Average ESP 2 Intake pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ5vLJh4z-WUWDJoC7xX4zOQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0MC0wMDUzLURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66550053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw",
                                            "ec618cd4-09bd-4871-96e9-908da6e7fd53",
                                            "OSX3_PIT-6655-0053-DAILY-AVG",
                                            "Average ESP 5 Intake Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQ1Ixh7L0JcUiW6ZCNpuf9UwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY1NS0wMDUzLURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66350053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw",
                                            "359133b9-28b1-4d52-a627-ea15bb4ee137",
                                            "OSX3_PIT-6635-0053-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQuTORNbEoUk2mJ-oVu07hNwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjYzNS0wMDUzLURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66450053Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw",
                                            "e1c76b56-91b8-4d75-8fbd-9a2b654706b3",
                                            "OSX3_PIT-6645-0053-DAILY-AVG",
                                            "Average ESP 3 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQVmvH4biRdU2PvZorZUcGswUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8T1NYM19QSVQtNjY0NS0wMDUzLURBSUxZLUFWRw/value"
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

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementsInstaceId" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId
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
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQCy0vflCw6EqHeGgTgQHZzQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjYzNS0wMDU1LURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66400055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw",
                                            "eb82a42e-1ce5-4a80-918d-c042170f0a96",
                                            "OSX3_PIT-6640-0055-DAILY-AVG",
                                            "Average ESP 2 Downhole Pressure Well 5 - 44HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQLqSC6-UcgEqRjcBCFw8KlgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0MC0wMDU1LURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66450055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw",
                                            "9d26aa29-63a1-414d-b18f-ffea97ff079b",
                                            "OSX3_PIT-6645-0055-DAILY-AVG",
                                            "Average ESP 3 Downhole Pressure Well 6 - 6HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQKaomnaFjTUGxj__ql_8HmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY0NS0wMDU1LURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66500055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw",
                                            "9aa56555-94ad-4180-a10f-b5cfdf43e2f0",
                                            "OSX3_PIT-6650-0055-DAILY-AVG",
                                            "Average ESP 4 Downhole Pressure Well 7 - 8H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVWWlmq2UgEGhD7XP30Pi8AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1MC0wMDU1LURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66550055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw",
                                            "765d0c75-ea0e-4d3c-a625-6552c4577f44",
                                            "OSX3_PIT-6655-0055-DAILY-AVG",
                                            "Average ESP 5 Downhole Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQdQxddg7qPE2mJWVSxFd_RAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY1NS0wMDU1LURBSUxZLUFWRw/value"
                                        },
                                        new object[] {
                                            osx3_66000055Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw",
                                            "8b13b755-feb6-4249-8373-4a09901f97a4",
                                            "OSX3_PIT-6660-0055-DAILY-AVG",
                                            "Average Downhole Pressure ESP 6 Well 9 - 10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQVbcTi7b-SUKDc0oJkB-XpAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDF8T1NYM19QSVQtNjY2MC0wMDU1LURBSUxZLUFWRw/value"
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

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementsInstaceId" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      pressurePDGId
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


                                var attributesData = new List<object[]>
                                    {
                                        new object[] {
                                            osx3_106068Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "58edd297-0baf-4410-88e8-f188e34c73ec",
                                            "OSX3_PIT-1060-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 9 - TBMT-10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQl9LtWK8LEESI6PGI40xz7AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH/value"
                                        },
                                        new object[] {
                                            osx3_105668Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "33222c9c-1bed-4e33-9542-d4d7fa393a6d",
                                            "OSX3_PIT-1056-68-DAILY-AVG",
                                            "Average Pressão na ANM Well 7 - TBMT-08H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQnCwiM-0bM06VQtTX-jk6bQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH/value"
                                        },
                                        new object[] {
                                            osx3_105468Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "055bdec5-20bd-4270-8770-cdf417514861",
                                            "OSX3_PIT-1054-68-DAILY-AVG",
                                            "Average Pressão ANM Well 5",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQxd5bBb0gcEKHcM30F1FIYQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH/value"
                                        },
                                        new object[] {
                                            osx3_105568Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "fca93775-1162-4a89-9fc0-0a7e001b64f4",
                                            "OSX3_PIT-1055-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 8 - TBMT-4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQdTep_GIRiUqfwAp-ABtk9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH/value"
                                        },
                                        new object[] {
                                            osx3_105368Id,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "b7f47103-1c5f-4b6a-a3b1-856c3559aee2",
                                            "OSX3_PIT-1053-68-DAILY-AVG",
                                            "Average Pressão/Well 4",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQA3H0t18cakujsYVsNVmu4gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQfE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH/value"
                                        }

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

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementsInstaceId" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      WHPId
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
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQByFmFWJHOE259H5LOKMfOAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAxQS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH002AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc",
                                            "50101096-feaa-4e30-adf4-8038b5ec899b",
                                            "DH_PI_002A-DAILY-AVG",
                                            "Average QAY-002 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQlhAQUKr-ME6t9IA4teyJmwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDAyQS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH004AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc",
                                            "b53730d1-b181-40ba-b27e-be563a6ce3ef",
                                            "DH_PI_004A-DAILY-AVG",
                                            "Average QAY-004 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQ0TA3tYGxukCyfr5WOmzj7wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA0QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH007AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc",
                                            "0d096b00-522b-47db-81c0-f46d5adeb508",
                                            "DH_PI_007A-DAILY-AVG",
                                            "Average QAY-007 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQAGsJDStS20eBwPRtWt61CAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDA3QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH011AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc",
                                            "8a69634b-008a-4c89-a549-26fcd3e5bdc5",
                                            "DH_PI_011A-DAILY-AVG",
                                            "Average QAY-011 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQS2NpiooAiUylSSb80-W9xQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDExQS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH012AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc",
                                            "c013028b-8c94-424f-8df5-be894b00e6b8",
                                            "DH_PI_012A-DAILY-AVG",
                                            "Average QAY-012 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQiwITwJSMT0KN9b6JSwDmuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDEyQS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH014AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc",
                                            "47c22a3c-5fdb-48b1-868f-7c4fc32ccbce",
                                            "DH_PI_014A-DAILY-AVG",
                                            "Average QAY-014 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQPCrCR9tfsUiGj3xPwyzLzgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE0QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH016AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc",
                                            "c9163e22-b7d2-4d9e-9fc0-6575ea65528e",
                                            "DH_PI_016A-DAILY-AVG",
                                            "Average QAY-016 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQIj4WydK3nk2fwGV16mVSjgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDE2QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH024AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc",
                                            "9ec2633a-ca26-4357-99c9-9a9fbde325c0",
                                            "DH_PI_024A-DAILY-AVG",
                                            "Average QAY-024 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQOmPCnibKV0OZyZqfveMlwAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDI0QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH032AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc",
                                            "18edec7a-80d1-4af6-be99-9ccdb533e2f8",
                                            "DH_PI_032A-DAILY-AVG",
                                            "Average QAY-032 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQeuztGNGA9kq-mZzNtTPi-AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDMyQS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH036AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc",
                                            "fb110617-8ec2-476e-baa5-57bd9723e07f",
                                            "DH_PI_036A-DAILY-AVG",
                                            "Average QAY-036 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQFwYR-8KObke6pVe9lyPgfwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM2QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH038AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc",
                                            "dbda428e-675d-484b-8a27-c406f228a252",
                                            "DH_PI_038A-DAILY-AVG",
                                            "Average QAY-038 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQjkLa211nS0iKJ8QG8iiiUgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDM4QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH045AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc",
                                            "0e30aab1-2094-474f-930d-5624aefe4290",
                                            "DH_PI_045A-DAILY-AVG",
                                            "Average QAY-045 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQsaowDpQgT0eTDVYkrv5CkAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ1QS1EQUlMWS1BVkc/value"
                                        },
                                        new object[] {
                                            DH046AId,
                                            "F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc",
                                            "21f8590c-0669-48a1-bd70-ee8b3d96455e",
                                            "DH_PI_046A-DAILY-AVG",
                                            "Average QAY-046 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQDFn4IWkGoUi9cO6LPZZFXgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1J8REhfUElfMDQ2QS1EQUlMWS1BVkc/value"
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

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementsInstaceId" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId
                                      });
                                }
                            }
                            else if (elementId == WHPId)
                            {

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
                        }
                    }
                }

            }
            else if (instanceKey.ToUpper() == "FRADE")
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
                    var instanceId = instances[0];
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

                }

            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
