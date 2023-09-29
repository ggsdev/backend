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
                                      bravoId
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
                                      bravoId
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
                                            "P50_PT-1210023R-DAILY-AVG",
                                            "db552fde-7f18-49a4-a23a-13025cdafdcc",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q3i9V2xh_pEmiOhMCXNr9zAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNSLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q3i9V2xh_pEmiOhMCXNr9zAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNSLURBSUxZLUFWRw/value"
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw",
                                            "P50_PT-1210023N-DAILY-AVG",
                                            "ef217ee7-ca3b-46c7-9438-5cb122ea63dd",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Q534h7zvKx0aUOFyxIupj3QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNOLURBSUxZLUFWRw/value"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw",
                                            "P50_PT-1210023B-DAILY-AVG",
                                            "f06801c4-bdd7-4ae1-89a7-455930913a17",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QxAFo8Ne94UqJp0VZMJE6FwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNCLURBSUxZLUFWRw/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "P50_PT-1210023A-DAILY-AVG",
                                            "cd8fd6a3-0bed-4b2e-bcbf-6f866d074a27",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QXdQhiWoSEUeedQbR8Ub06QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNTLURBSUxZLUFWRw",
                                            "P50_PT-1210023S-DAILY-AVG",
                                            "8921d45d-126a-4711-9e75-06d1f146f4e9",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4Qo9aPze0LLku8v2-GbQdKJwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNBLURBSUxZLUFWRw/value"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw",
                                            "P50_PT-1210023D-DAILY-AVG",
                                            "3522d01d-e395-4d3e-85bc-bc291311c399",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QHdAiNZXjPk2FvLwpExHDmQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMjNELURBSUxZLUFWRw/value"
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
                                      PDGPressureId
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
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qgx3m_tWMTEqz5mw5k7RMEgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJMLURBSUxZLUFWRw/value"
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw",
                                            "P50_PT-1210012M-DAILY-AVG",
                                            "3b59b446-80c4-4951-9b7f-b96eed0bba7a",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRrRZO8SAUUmbf7lu7Qu6egUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJNLURBSUxZLUFWRw/value"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw",
                                            "P50_PT-1210012R-DAILY-AVG",
                                            "167f61c5-73f7-4155-ad69-b8558580dbb8",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QxWF_FvdzVUGtabhVhYDbuAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJSLURBSUxZLUFWRw/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw",
                                            "P50_PT-1210012N-DAILY-AVG",
                                            "715acdcb-bf12-47c5-82ea-104c0db2bf72",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qy81acRK_xUeC6hBMDbK_cgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJOLURBSUxZLUFWRw/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw",
                                            "P50_PT-1210012B-DAILY-AVG",
                                            "69f5f7db-ea9b-4b75-9624-70ff2c400b53",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q2_f1aZvqdUuWJHD_LEALUwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJCLURBSUxZLUFWRw/value"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw",
                                            "P50_PT-1210012A-DAILY-AVG",
                                            "64fd2eeb-b2d3-4023-a67d-4c551148d324",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Q6y79ZNOyI0CmfUxVEUjTJAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJBLURBSUxZLUFWRw/value"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw",
                                            "P50_PT-1210012S-DAILY-AVG",
                                            "48aab444-fb25-49ee-af97-6655e747df07",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QRLSqSCX77kmvl2ZV50ffBwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJTLURBSUxZLUFWRw/value"
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw",
                                            "P50_PT-1210012D-DAILY-AVG",
                                            "4b42a5af-b636-4ab3-b43d-b5a4a1dbfd79",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4Qr6VCSza2s0q0PbWkodv9eQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDF8UDUwX1BULTEyMTAwMTJELURBSUxZLUFWRw/value"
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
                                      TPTPressureId
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
                                            "P50_FT-1231010L-DAILY-AVG",
                                            "d391f4f8-6945-49e9-a928-1699b53db89e",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q-PSR00Vp6UmpKBaZtT24ngUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q-PSR00Vp6UmpKBaZtT24ngUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH/value"
                                        },

                                      new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "P50_FT-1231010M-DAILY-AVG",
                                            "bb2b48b1-9d04-48bf-92bf-2f8688dfadf6",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QsUgruwSdv0iSvy-GiN-t9gUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH/value"
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "P50_FT-1231010R-DAILY-AVG",
                                            "679b3e8c-b379-4336-8ba1-4b29d6ab4bf4",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QjD6bZ3mzNkOLoUsp1qtL9AUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "P50_FT-1231010N-DAILY-AVG",
                                            "d04402d6-1da3-4c8f-ad1e-c75e5c7cf43c",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q1gJE0KMdj0ytHsdeXHz0PAUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH/value"
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "P50_FT-1231010B-DAILY-AVG",
                                            "8a8d2d21-96e6-463f-8f4a-5e8453721dab",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QIS2NiuaWP0aPSl6EU3IdqwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH/value"
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "P50_FT-1231010A-DAILY-AVG",
                                            "2180c9e7-299d-41af-9156-10d4c659d75f",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Q58mAIZ0pr0GRVhDUxlnXXwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH/value"
                                        },



                                         new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "P50_FT-1231010S-DAILY-AVG",
                                            "de984ba3-c1fe-4563-afbe-c6f9b9b6d976",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qo0uY3v7BY0Wvvsb5ubbZdgUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH/value"
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                                                                "F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "P50_FT-1231010D-DAILY-AVG",
                                            "b5d9768f-164a-45d8-a3ce-baea78c024c3",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1AbEcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4Qj3bZtUoW2EWjzrrqeMAkwwUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVHxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH/value"
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
                                      GASLiftId
                                      });
                                }

                            }
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
