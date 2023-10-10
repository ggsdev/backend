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
            var today = DateTime.UtcNow.AddHours(-3);

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
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
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
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes",
                                "Pressão",
                                "Pressão Intake ESP"
                            },
                            new object[] {
                                pressurePDGId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE",
                                "7eedaab9-0080-11ee-b18f-0050568cb915",
                                "Pressure PDG 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE/attributes",
                                "Pressão",
                                "Pressão PDG 1"
                            },
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "a8b5764a-0080-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ/attributes",
                                "Pressão",
                                "Pressão WH"
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
                            var elementCategoryParamenter = element[7];
                            var elementParamenter = element[8];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId", "CategoryParameter", "Parameter" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              bravoId,
                              elementCategoryParamenter,
                              elementParamenter,
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
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAWg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1My1EQUlMWS1BVkc",
                                            "189aea96-6ef0-4f01-9afb-013c231f8877",
                                            "OSX3_PIT-6660-0053-DAILY-AVG",
                                            "Average ESP 6 Intake Pressure Well 9 - 10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAWg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAWg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1My1EQUlMWS1BVkc/value",
                                            "7-TBMT-10H-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66500053Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAWA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1My1EQUlMWS1BVkc",
                                            "fefcd4ca-431c-4449-adf8-9484a3eb0833",
                                            "OSX3_PIT-6650-0053-DAILY-AVG",
                                            "Average ESP 4 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAWA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAWA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1My1EQUlMWS1BVkc/value",
                                            "7-TBMT-8H-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66400053Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAWQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1My1EQUlMWS1BVkc",
                                            "87c9f2e6-fe8c-4559-8326-80bbc57e3339",
                                            "OSX3_PIT-6640-0053-DAILY-AVG",
                                            "Average ESP 2 Intake pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAWQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAWQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1My1EQUlMWS1BVkc/value",
                                            "9-OGX-44HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66550053Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAVg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1My1EQUlMWS1BVkc",
                                            "ec618cd4-09bd-4871-96e9-908da6e7fd53",
                                            "OSX3_PIT-6655-0053-DAILY-AVG",
                                            "Average ESP 5 Intake Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAVg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAVg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1My1EQUlMWS1BVkc/value",
                                            "7-TBMT-4HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66350053Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhATw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1My1EQUlMWS1BVkc",
                                            "359133b9-28b1-4d52-a627-ea15bb4ee137",
                                            "OSX3_PIT-6635-0053-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhATw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhATw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1My1EQUlMWS1BVkc/value",
                                            "7-TBMT-2HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66450053Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAVw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1My1EQUlMWS1BVkc",
                                            "e1c76b56-91b8-4d75-8fbd-9a2b654706b3",
                                            "OSX3_PIT-6645-0053-DAILY-AVG",
                                            "Average ESP 3 Intake Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAVw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1My1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAVw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1My1EQUlMWS1BVkc/value",
                                            "7-TBMT-6HP-RJS",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                            "F1DPxTrxlLuvikS_f2CTqj2YhATg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1NS1EQUlMWS1BVkc",
                                            "7e2f2d0b-b050-4ae8-8778-68138101d9cd",
                                            "OSX3_PIT-6635-0055-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhATg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhATg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2MzUtMDA1NS1EQUlMWS1BVkc/value",
                                            "7-TBMT-2HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66400055Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAVA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1NS1EQUlMWS1BVkc",
                                            "eb82a42e-1ce5-4a80-918d-c042170f0a96",
                                            "OSX3_PIT-6640-0055-DAILY-AVG",
                                            "Average ESP 2 Downhole Pressure Well 5 - 44HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAVA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAVA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDAtMDA1NS1EQUlMWS1BVkc/value",
                                            "9-OGX-44HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66450055Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAUg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1NS1EQUlMWS1BVkc",
                                            "9d26aa29-63a1-414d-b18f-ffea97ff079b",
                                            "OSX3_PIT-6645-0055-DAILY-AVG",
                                            "Average ESP 3 Downhole Pressure Well 6 - 6HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAUg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAUg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NDUtMDA1NS1EQUlMWS1BVkc/value",
                                            "7-TBMT-6HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66500055Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAUw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1NS1EQUlMWS1BVkc",
                                            "9aa56555-94ad-4180-a10f-b5cfdf43e2f0",
                                            "OSX3_PIT-6650-0055-DAILY-AVG",
                                            "Average ESP 4 Downhole Pressure Well 7 - 8H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAUw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAUw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTAtMDA1NS1EQUlMWS1BVkc/value",
                                            "7-TBMT-8H-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66550055Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAUQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1NS1EQUlMWS1BVkc",
                                            "765d0c75-ea0e-4d3c-a625-6552c4577f44",
                                            "OSX3_PIT-6655-0055-DAILY-AVG",
                                            "Average ESP 5 Downhole Pressure Well 8 - 4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAUQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAUQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NTUtMDA1NS1EQUlMWS1BVkc/value",
                                            "7-TBMT-4HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_66000055Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAVQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1NS1EQUlMWS1BVkc",
                                            "8b13b755-feb6-4249-8373-4a09901f97a4",
                                            "OSX3_PIT-6660-0055-DAILY-AVG",
                                            "Average Downhole Pressure ESP 6 Well 9 - 10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAVQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1NS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAVQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTY2NjAtMDA1NS1EQUlMWS1BVkc/value",
                                            "7-TBMT-10H-RJS",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      pressurePDGId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAYA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "58edd297-0baf-4410-88e8-f188e34c73ec",
                                            "OSX3_PIT-1060-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 9 - TBMT-10HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAYA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAYA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNjAtNjgtREFJTFktQVZHvalue",
                                            "7-TBMT-10H-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_105668Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAXg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "33222c9c-1bed-4e33-9542-d4d7fa393a6d",
                                            "OSX3_PIT-1056-68-DAILY-AVG",
                                            "Average Pressão na ANM Well 7 - TBMT-08H",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAXg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAXg8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTYtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-8H-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_105468Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAXw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "055bdec5-20bd-4270-8770-cdf417514861",
                                            "OSX3_PIT-1054-68-DAILY-AVG",
                                            "Average Pressão ANM Well 5",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAXw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAXw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTQtNjgtREFJTFktQVZH/value",
                                            "9-OGX-44HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_105568Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAXA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "fca93775-1162-4a89-9fc0-0a7e001b64f4",
                                            "OSX3_PIT-1055-68-DAILY-AVG",
                                            "Average Pressão  ANM Well 8 - TBMT-4HP",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAXA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAXA8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTUtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-4HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_105368Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAWw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "b7f47103-1c5f-4b6a-a3b1-856c3559aee2",
                                            "OSX3_PIT-1053-68-DAILY-AVG",
                                            "Average Pressão/Well 4",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAWw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAWw8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTMtNjgtREFJTFktQVZH/value",
                                            "7-TBMT-2HP-RJS",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            osx3_105268Id,
                                            "F1DPxTrxlLuvikS_f2CTqj2YhAXQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH",
                                            "59c39246-a902-4c0c-9fcb-88ed3d2cbecf",
                                            "OSX3_PIT-1052-68-DAILY-AVG",
                                            "Average Pressão ANM Well 6",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPxTrxlLuvikS_f2CTqj2YhAXQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPxTrxlLuvikS_f2CTqj2YhAXQ8AAAUFJSSlRTUlZWTTA2XE9TWDNfUElULTEwNTItNjgtREFJTFktQVZH/value",
                                            "7-TBMT-6HP-RJS",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      WHPId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes",
                                "Pressão",
                                "Pressão Intake ESP"

                            },
                            //new object[] {
                            //    vibrationId,
                            //    "F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                            //    "c1f96c32-509f-11ee-b198-0050568cb915",
                            //    "Vibração",
                            //    "",
                            //    "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                            //    "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw/attributes"
                            //},
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "6e648f8a-0084-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ/attributes",
                                "Pressão",
                                "Pressão WH"
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
                            var elementCategoryParamenter = element[7];
                            var elementParamenter = element[8];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId", "CategoryParameter", "Parameter" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              polvoId,
                              elementCategoryParamenter,
                              elementParamenter,
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
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgvAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMUEtREFJTFktQVZH",
                                            "15662107-4762-4d38-b9f4-7e4b38a31f38",
                                            "DH_PI_001A-DAILY-AVG",
                                            "Average QAY-001 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgvAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMUEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgvAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMUEtREFJTFktQVZH/value",
                                            "POL-001-A",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH002AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgvQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMkEtREFJTFktQVZH",
                                            "50101096-feaa-4e30-adf4-8038b5ec899b",
                                            "DH_PI_002A-DAILY-AVG",
                                            "Average QAY-002 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgvQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgvQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwMkEtREFJTFktQVZH/value",
                                            "POL-002-By",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH004AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgvgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwNEEtREFJTFktQVZH",
                                            "b53730d1-b181-40ba-b27e-be563a6ce3ef",
                                            "DH_PI_004A-DAILY-AVG",
                                            "Average QAY-004 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgvgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwNEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgvgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwNEEtREFJTFktQVZH/value",
                                            "POL-004-Cx",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH007AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgvwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwN0EtREFJTFktQVZH",
                                            "0d096b00-522b-47db-81c0-f46d5adeb508",
                                            "DH_PI_007A-DAILY-AVG",
                                            "Average QAY-007 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgvwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwN0EtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgvwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAwN0EtREFJTFktQVZH/value",
                                            "POL-007-Gx",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH011AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgwAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMUEtREFJTFktQVZH",
                                            "8a69634b-008a-4c89-a549-26fcd3e5bdc5",
                                            "DH_PI_011A-DAILY-AVG",
                                            "Average QAY-011 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgwAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMUEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgwAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMUEtREFJTFktQVZH/value",
                                            "POL-011-Dy",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH012AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgwQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMkEtREFJTFktQVZH",
                                            "c013028b-8c94-424f-8df5-be894b00e6b8",
                                            "DH_PI_012A-DAILY-AVG",
                                            "Average QAY-012 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgwQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgwQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxMkEtREFJTFktQVZH/value",
                                            "POL-012-R",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH014AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgwgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNEEtREFJTFktQVZH",
                                            "47c22a3c-5fdb-48b1-868f-7c4fc32ccbce",
                                            "DH_PI_014A-DAILY-AVG",
                                            "Average QAY-014 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgwgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgwgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNEEtREFJTFktQVZH/value",
                                            "POL-014-T",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH016AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgxAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNkEtREFJTFktQVZH",
                                            "c9163e22-b7d2-4d9e-9fc0-6575ea65528e",
                                            "DH_PI_016A-DAILY-AVG",
                                            "Average QAY-016 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgxAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgxAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAxNkEtREFJTFktQVZH/value",
                                            "POL-016-W",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH024AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgxgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAyNEEtREFJTFktQVZH",
                                            "9ec2633a-ca26-4357-99c9-9a9fbde325c0",
                                            "DH_PI_024A-DAILY-AVG",
                                            "Average QAY-024 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgxgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAyNEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgxgIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAyNEEtREFJTFktQVZH/value",
                                            "POL-024-Oy",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH032AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgxwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzMkEtREFJTFktQVZH",
                                            "18edec7a-80d1-4af6-be99-9ccdb533e2f8",
                                            "DH_PI_032A-DAILY-AVG",
                                            "Average QAY-032 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgxwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzMkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgxwIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzMkEtREFJTFktQVZH/value",
                                            "POL-032-Xc",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH036AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgyAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzNkEtREFJTFktQVZH",
                                            "fb110617-8ec2-476e-baa5-57bd9723e07f",
                                            "DH_PI_036A-DAILY-AVG",
                                            "Average QAY-036 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgyAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzNkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgyAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzNkEtREFJTFktQVZH/value",
                                            "POL-036-Pj",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH038AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgyQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzOEEtREFJTFktQVZH",
                                            "dbda428e-675d-484b-8a27-c406f228a252",
                                            "DH_PI_038A-DAILY-AVG",
                                            "Average QAY-038 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgyQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzOEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgyQIAAAUFJSSlBTUlZWTTA5XERIX1BJXzAzOEEtREFJTFktQVZH/value",
                                            "POL-038-Za",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH045AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgywIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NUEtREFJTFktQVZH",
                                            "0e30aab1-2094-474f-930d-5624aefe4290",
                                            "DH_PI_045A-DAILY-AVG",
                                            "Average QAY-045 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgywIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NUEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgywIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NUEtREFJTFktQVZH/value",
                                            "POL-045-L",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            DH046AId,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgzAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NkEtREFJTFktQVZH",
                                            "21f8590c-0669-48a1-bd70-ee8b3d96455e",
                                            "DH_PI_046A-DAILY-AVG",
                                            "Average QAY-046 INTAKE PRESSURE",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgzAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgzAIAAAUFJSSlBTUlZWTTA5XERIX1BJXzA0NkEtREFJTFktQVZH/value",
                                            "POL-046-K",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgzQIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDEtREFJTFktQVZH",
                                            "c8be0ed5-6536-4a1f-8ee3-35c84a55b2a2",
                                            "PXT_001-DAILY-AVG",
                                            "Average Flowline FA1-001 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgzQIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgzQIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDEtREFJTFktQVZH/value",
                                            "POL-001-A",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT002Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgzgIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDItREFJTFktQVZH",
                                            "9e14fadd-891b-4da2-9253-56a0474ed8c3",
                                            "PXT_002-DAILY-AVG",
                                            "Average Flowline FA1-002 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgzgIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgzgIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDItREFJTFktQVZH/value",
                                            "POL-002-By",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT004Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXgzwIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDQtREFJTFktQVZH",
                                            "b2d7c802-950f-4078-ac13-5f2c0042ba9f",
                                            "PXT_004-DAILY-AVG",
                                            "Average Flowline FA1-004 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXgzwIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXgzwIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDQtREFJTFktQVZH/value",
                                            "POL-004-Cx",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT007Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg0AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDctREFJTFktQVZH",
                                            "da3757ca-ff25-4346-8f16-9fff2117f4eb",
                                            "PXT_007-DAILY-AVG",
                                            "Average Flowline FA1-007 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg0AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDctREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg0AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMDctREFJTFktQVZH/value",
                                            "POL-007-Gx",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT011Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg0QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTEtREFJTFktQVZH",
                                            "50323c77-0ba3-4bb6-93ef-f8d00c70ae46",
                                            "PXT_011-DAILY-AVG",
                                            "Average Flowline FA1-011 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg0QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg0QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTEtREFJTFktQVZH/value",
                                            "POL-011-Dy",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT012Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg0gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTItREFJTFktQVZH",
                                            "2411a314-b513-4b89-8d2e-55fcc2c22b2b",
                                            "PXT_012-DAILY-AVG",
                                            "Average Flowline FA1-012 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg0gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg0gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTItREFJTFktQVZH/value",
                                            "POL-012-R",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT014Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg0wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTQtREFJTFktQVZH",
                                            "4be3867b-a50b-45a5-a0f5-04202e7a05e8",
                                            "PXT_014-DAILY-AVG",
                                            "Average Flowline FA1-014 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg0wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg0wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTQtREFJTFktQVZH/value",
                                            "POL-014-T",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT016Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg1QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTYtREFJTFktQVZH",
                                            "9dc9cf97-e3ad-42b0-8e2b-d347d2adf41d",
                                            "PXT_016-DAILY-AVG",
                                            "Average Flowline FA1-016 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg1QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg1QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMTYtREFJTFktQVZH/value",
                                            "POL-016-W",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT024Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg1wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMjQtREFJTFktQVZH",
                                            "dc3a7616-7f1b-45e6-98ae-112f13e87b6a",
                                            "PXT_024-DAILY-AVG",
                                            "Average Flowline FA2-024 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg1wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMjQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg1wIAAAUFJSSlBTUlZWTTA5XFBYVF8wMjQtREFJTFktQVZH/value",
                                            "POL-024-Oy",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT032Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg2AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzItREFJTFktQVZH",
                                            "ee8dcb35-7b6e-41cf-93b7-fe0eca969591",
                                            "PXT_032-DAILY-AVG",
                                            "Average Flowline FA1-032 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg2AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg2AIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzItREFJTFktQVZH/value",
                                            "POL-032-Xc",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT036Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg2QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzYtREFJTFktQVZH",
                                            "06869b1e-1232-4dc9-87a4-820f09111d95",
                                            "PXT_036-DAILY-AVG",
                                            "Average Flowline FA1-036 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg2QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg2QIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzYtREFJTFktQVZH/value",
                                            "POL-036-Pj",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT038Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg2gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzgtREFJTFktQVZH",
                                            "910e8496-88d0-4855-b7e6-beeefb92f3a6",
                                            "PXT_038-DAILY-AVG",
                                            "Average Flowline FA1-038 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg2gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg2gIAAAUFJSSlBTUlZWTTA5XFBYVF8wMzgtREFJTFktQVZH/value",
                                            "POL-038-Za",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT045Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg3AIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDUtREFJTFktQVZH",
                                            "4a86762c-0ae6-48a5-9da1-665af92152c8",
                                            "PXT_045-DAILY-AVG",
                                            "Average Flowline FA1-045 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg3AIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDUtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg3AIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDUtREFJTFktQVZH/value",
                                            "POL-045-L",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            PXT046Id,
                                            "F1DPuwo-zFy4eEKLzH7E-bYcXg3QIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDYtREFJTFktQVZH",
                                            "ebb57ce2-e0a8-4df1-90a2-78ec91dc5f7b",
                                            "PXT_046-DAILY-AVG",
                                            "Average Flowline FA1-046 Pressure",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPuwo-zFy4eEKLzH7E-bYcXg3QIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDYtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPuwo-zFy4eEKLzH7E-bYcXg3QIAAAUFJSSlBTUlZWTTA5XFBYVF8wNDYtREFJTFktQVZH/value",
                                            "POL-046-K",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      intakePressureId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                        var InjectionWaterId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                PDGPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "7247ad3d-27ec-11ee-b194-0050568cc6e1",
                                "PDG Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE/attributes",
                                "Pressão",
                                "Pressão PDG 1"
                            },
                            new object[] {
                                TPTPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "a1158244-27ef-11ee-b194-0050568cc6e1",
                                "TPT Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE/attributes",
                                "Pressão",
                                "Pressão WH"
                            },
                            new object[] {
                                GASLiftId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "689796c7-27ff-11ee-b194-0050568cc6e1",
                                "Vazão de gas lift",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA/attributes",
                                "Vazão",
                                "Vazão de Gas Lift , Alocação de Gas Lift"
                            },
                            new object[] {
                                InjectionWaterId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQxQbKfy8q7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIExJTkhBIElOSkXDh8ODTyDDgUdVQSBQT1IgUE_Dh08",
                                "7fca06c5-2a2f-11ee-b194-0050568cc6e1",
                                "Vazão Linha injeção água por poço",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQxQbKfy8q7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIExJTkhBIElOSkXDh8ODTyDDgUdVQSBQT1IgUE_Dh08",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQxQbKfy8q7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIExJTkhBIElOSkXDh8ODTyDDgUdVQSBQT1IgUE_Dh08/attributes",
                                "Vazão",
                                "Vazão de injeção de água , Alocação de injeção de água"
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
                            var elementCategoryParamenter = element[7];
                            var elementParamenter = element[8];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId", "CategoryParameter", "Parameter" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              forteId,
                              elementCategoryParamenter,
                              elementParamenter,
                              });

                            if (elementId == PDGPressureId)
                            {

                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygOT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1ItREFJTFktQVZH",
                                            "db552fde-7f18-49a4-a23a-13025cdafdcc",
                                            "P50_PT-1210023R-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygOT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1ItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygOT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1ItREFJTFktQVZH/value",
                                            "ABL-16HP",
                                            true,
                                            true,
                                            today
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygOz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM04tREFJTFktQVZH",
                                            "ef217ee7-ca3b-46c7-9438-5cb122ea63dd",
                                            "P50_PT-1210023N-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygOz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM04tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygOz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM04tREFJTFktQVZH/value",
                                            "ABL-24HP",
                                            true,
                                            true,
                                            today
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygQz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0ItREFJTFktQVZH",
                                            "f06801c4-bdd7-4ae1-89a7-455930913a17",
                                            "P50_PT-1210023B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygQz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0ItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygQz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0ItREFJTFktQVZH/value",
                                            "ABL-87HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygNT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0EtREFJTFktQVZH",
                                            "cd8fd6a3-0bed-4b2e-bcbf-6f866d074a27",
                                            "P50_PT-1210023A-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygNT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0EtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygNT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0EtREFJTFktQVZH/value",
                                            "ABL-81HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygQj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1MtREFJTFktQVZH",
                                            "8921d45d-126a-4711-9e75-06d1f146f4e9",
                                            "P50_PT-1210023S-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygQj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1MtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygQj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM1MtREFJTFktQVZH/value",
                                            "ABL-84HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygVUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEItREFJTFktQVZH",
                                            "8921d45d-126a-4711-9e75-06d1f146f4e9",
                                            "P50_PT-1210024B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygVUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygVUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEItREFJTFktQVZH/value",
                                            "ABL-82HA",
                                            true,
                                            true,
                                            today
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygNj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0QtREFJTFktQVZH",
                                            "3522d01d-e395-4d3e-85bc-bc291311c399",
                                            "P50_PT-1210023D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygNj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0QtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygNj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0QtREFJTFktQVZH/value",
                                            "AB-134HPA",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygPj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0wtREFJTFktQVZH",
                                            "49aa1a96-4a29-4466-a0df-e87a6dbcd767",
                                            "P50_PT-1210023L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygPj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0wtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygPj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM0wtREFJTFktQVZH/value",
                                            "ABL-54HP",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygWEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNFAtREFJTFktQVZH",
                                            "",
                                            "P50_PT-1210024P-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygWEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNFAtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygWEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNFAtREFJTFktQVZH/value",
                                            "ABL-88HA",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygOD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM00tREFJTFktQVZH",
                                            "92591c6b-3f13-4393-b46d-5aa04febaa54",
                                            "P50_PT-1210023M-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygOD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM00tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygOD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyM00tREFJTFktQVZH/value",
                                            "ABL-13HP",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygU0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEctREFJTFktQVZH",
                                            "",
                                            "P50_PT-1210024G-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygU0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEctREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygU0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAyNEctREFJTFktQVZH/value",
                                            "ABL-18HP",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDGPressureId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }
                            }

                            else if (elementId == TPTPressureId)
                            {
                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygXj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkwtREFJTFktQVZH",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "P50_PT-1210012L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygXj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygXj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkwtREFJTFktQVZH/value",
                                            "ABL-54HP",
                                            true,
                                            true,
                                            today
                                        },
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygSkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkgtREFJTFktQVZH",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "P50_PT-1210062H-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygSkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygSkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkgtREFJTFktQVZH/value",
                                            "ABL-52HPA",
                                            true,
                                            true,
                                            today
                                        },
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygUUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MlAtREFJTFktQVZH",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "P50_PT-1210062P-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygUUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MlAtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygUUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MlAtREFJTFktQVZH/value",
                                            "ABL-88HA",
                                            true,
                                            true,
                                            today
                                        },
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygSEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkQtREFJTFktQVZH",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "P50_PT-1210062D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygSEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygSEAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkQtREFJTFktQVZH/value",
                                            "ABL-36HP",
                                            true,
                                            true,
                                            today
                                        },
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygRkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkwtREFJTFktQVZH",
                                            "fee61d83-8cd5-4a4c-b3e6-6c3993b44c12",
                                            "P50_PT-1210062L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygRkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygRkAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkwtREFJTFktQVZH/value",
                                            "ABL-22HPA",
                                            true,
                                            true,
                                            today
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygWD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk0tREFJTFktQVZH",
                                            "3b59b446-80c4-4951-9b7f-b96eed0bba7a",
                                            "P50_PT-1210012M-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygWD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk0tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygWD8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk0tREFJTFktQVZH/value",
                                            "ABL-13HP",
                                            true,
                                            true,
                                            today
                                        },
                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygRUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkctREFJTFktQVZH",
                                            "3b59b446-80c4-4951-9b7f-b96eed0bba7a",
                                            "P50_PT-1210062G-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygRUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkctREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygRUAAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkctREFJTFktQVZH/value",
                                            "ABL-18HP",
                                            true,
                                            true,
                                            today
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygWT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlItREFJTFktQVZH",
                                            "167f61c5-73f7-4155-ad69-b8558580dbb8",
                                            "P50_PT-1210012R-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygWT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygWT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlItREFJTFktQVZH/value",
                                            "ABL-16HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygWz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk4tREFJTFktQVZH",
                                            "715acdcb-bf12-47c5-82ea-104c0db2bf72",
                                            "P50_PT-1210012N-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygWz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk4tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygWz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMk4tREFJTFktQVZH/value",
                                            "ABL-24HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygYz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkItREFJTFktQVZH",
                                            "69f5f7db-ea9b-4b75-9624-70ff2c400b53",
                                            "P50_PT-1210012B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygYz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygYz8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkItREFJTFktQVZH/value",
                                            "ABL-87HP",
                                            true,
                                            true,
                                            today
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygVT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkEtREFJTFktQVZH",
                                            "64fd2eeb-b2d3-4023-a67d-4c551148d324",
                                            "P50_PT-1210012A-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygVT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygVT8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkEtREFJTFktQVZH/value",
                                            "ABL-81HP",
                                            true,
                                            true,
                                            today
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygYj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlMtREFJTFktQVZH",
                                            "48aab444-fb25-49ee-af97-6655e747df07",
                                            "P50_PT-1210012S-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygYj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlMtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygYj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMlMtREFJTFktQVZH/value",
                                            "ABL-84HP",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygT0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkItREFJTFktQVZH",
                                            "48aab444-fb25-49ee-af97-6655e747df07",
                                            "P50_PT-1210062B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygT0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygT0AAAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDA2MkItREFJTFktQVZH/value",
                                            "ABL-82HA",
                                            true,
                                            true,
                                            today
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygVj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkQtREFJTFktQVZH",
                                            "4b42a5af-b636-4ab3-b43d-b5a4a1dbfd79",
                                            "P50_PT-1210012D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygVj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygVj8AAAUFJSSjA5U1JWVk0wMlxQNTBfUFQtMTIxMDAxMkQtREFJTFktQVZH/value",
                                            "AB-134HPA",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      TPTPressureId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }

                            }

                            else if (elementId == GASLiftId)
                            {
                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygvj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH",
                                            "d391f4f8-6945-49e9-a928-1699b53db89e",
                                            "P50_FT-1231010L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygvj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygvj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEwtREFJTFktQVZH/value",
                                            "ABL-54HP",
                                            true,
                                            true,
                                            today
                                        },

                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6yguD8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "bb2b48b1-9d04-48bf-92bf-2f8688dfadf6",
                                            "P50_FT-1231010M-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6yguD8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6yguD8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME0tREFJTFktQVZH/value",
                                            "ABL-13HP",
                                            true,
                                            true,
                                            today
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6yguT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "679b3e8c-b379-4336-8ba1-4b29d6ab4bf4",
                                            "P50_FT-1231010R-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6yguT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6yguT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFItREFJTFktQVZH/value",
                                            "ABL-16HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6yguz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "d04402d6-1da3-4c8f-ad1e-c75e5c7cf43c",
                                            "P50_FT-1231010N-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6yguz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6yguz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxME4tREFJTFktQVZH/value",
                                            "ABL-24HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygwz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "8a8d2d21-96e6-463f-8f4a-5e8453721dab",
                                            "P50_FT-1231010B-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygwz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygwz8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEItREFJTFktQVZH/value",
                                            "ABL-87HP",
                                            true,
                                            true,
                                            today
                                        },

                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygtT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "2180c9e7-299d-41af-9156-10d4c659d75f",
                                            "P50_FT-1231010A-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygtT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygtT8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEEtREFJTFktQVZH/value"
                                            ,"ABL-81HP",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygwj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "de984ba3-c1fe-4563-afbe-c6f9b9b6d976",
                                            "P50_FT-1231010S-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygwj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygwj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMFMtREFJTFktQVZH/value",
                                            "ABL-84HP",
                                            true,
                                            true,
                                            today
                                        },

                                          new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygtj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "b5d9768f-164a-45d8-a3ce-baea78c024c3",
                                            "P50_FT-1231010D-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygtj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygtj8AAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIzMTAxMEQtREFJTFktQVZH/value",
                                            "AB-134HPA",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      GASLiftId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }

                            }

                            else if (elementId == InjectionWaterId)
                            {




                                var attributesData = new List<object[]>
                                {
                                     new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygPUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUgtREFJTFktQVZH",
                                            "",
                                            "P50_FT-1210001H-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygPUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUgtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygPUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUgtREFJTFktQVZH/value",
                                            "ABL-52HPA",
                                            true,
                                            true,
                                            today
                                        },

                                      new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygREAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMi1EQUlMWS1BVkc",
                                            "",
                                            "P50_FT-1210012-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygREAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygREAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMi1EQUlMWS1BVkc/value",
                                            "ABL-88HA",
                                            true,
                                            true,
                                            today
                                        },

                                       new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygO0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwOC1EQUlMWS1BVkc",
                                            "",
                                            "P50_FT-1210008-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygO0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwOC1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygO0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwOC1EQUlMWS1BVkc/value",
                                            "ABL-36HP",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygOEAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUwtREFJTFktQVZH",
                                            "",
                                            "P50_FT-1210001L-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygOEAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUwtREFJTFktQVZH",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygOEAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwMUwtREFJTFktQVZH/value",
                                            "ABL-22HPA",
                                            true,
                                            true,
                                            today
                                        },

                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygN0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMS1EQUlMWS1BVkc",
                                            "",
                                            "P50_FT-1210011-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygN0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMS1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygN0AAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAxMS1EQUlMWS1BVkc/value",
                                            "ABL-18HP",
                                            true,
                                            true,
                                            today
                                        },
                                         new object[] {
                                            Guid.NewGuid(),
                                            "F1DPXZtDP9A2dkCslnDK15S6ygQUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwNi1EQUlMWS1BVkc",
                                            "",
                                            "P50_FT-1210006-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPXZtDP9A2dkCslnDK15S6ygQUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwNi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPXZtDP9A2dkCslnDK15S6ygQUAAAAUFJSSjA5U1JWVk0wMlxQNTBfRlQtMTIxMDAwNi1EQUlMWS1BVkc/value",
                                            "ABL-82HA",
                                            true,
                                            true,
                                            today
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
                                    var attributeWellName = attribute[7];
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      GASLiftId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                        var GFL1Id = Guid.NewGuid();
                        var GFL4Id = Guid.NewGuid();
                        var GFL6Id = Guid.NewGuid();
                        var WFL1Id = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                PDG1Id,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx",
                                "02126f19-2589-11ee-b197-0050568cf50d",
                                "Pressao de Fundo de Poço 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQGW8SAokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAx/attributes",
                                "Pressão",
                                "Pressão PDG 1"
                            },
                            new object[] {
                                PDG2Id,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy",
                                "0e21dcb6-2589-11ee-b197-0050568cf50d",
                                "Pressao de Fundo de Poço 2",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQttwhDokl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBERSBGVU5ETyBERSBQT8OHTyAy/attributes",
                                "Pressão",
                                "Pressão PDG 2"
                            },
                            new object[] {
                                SSPCVId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg",
                                "18602e1e-2589-11ee-b197-0050568cf50d",
                                "Pressao Montante SSPCV",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQHi5gGIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcUFJFU1NBTyBNT05UQU5URSBTU1BDVg/attributes",
                                "Pressão",
                                "Pressão WH"

                            },
                            new object[] {
                                VConeId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "44761040-2589-11ee-b197-0050568cf50d",
                                "Vazao VCone",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes",
                                "Vazão",
                                "Vazão de Gas Lift"

                            },
                            new object[] {
                                GFL1Id,
                                "Ñão Fornecido1",
                                "",
                                "Vazao GFL1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes",
                                "Vazão",
                                "Vazão da GFL1"

                            },
                            new object[] {
                                GFL4Id,
                                "Ñão Fornecido2",
                                "",
                                "Vazao GFL4",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes",
                                "Vazão",
                                "Vazão da GFL4"

                            },
                            new object[] {
                                GFL6Id,
                                "Ñão Fornecido3",
                                "",
                                "Vazao GFL6",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes",
                                "Vazão",
                                "Vazão da GFL6"

                            },
                            new object[] {
                                WFL1Id,
                                "Ñão Fornecido4",
                                "",
                                "Vazao WFL1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQQBB2RIkl7hGxlwBQVoz1DQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREVcVkFaQU8gVkNPTkU/attributes",
                                "Vazão",
                                "Vazão da WFL1"

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
                            var elementCategoryParamenter = element[7];
                            var elementParamenter = element[8];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId", "CategoryParameter", "Parameter" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              fradeId,
                              elementCategoryParamenter,
                              elementParamenter,
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
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wBlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "4baa79a8-b90e-4544-a237-cdd2e1f99dfb",
                                            "FRSS-PI-ODP4-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wBlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wBlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP4",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MUP5086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7w_1EAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "dbf02bd8-5c47-491d-804a-ab2412f3ab1d",
                                            "FRSS-PI-MUP5-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7w_1EAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7w_1EAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP5",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MDP2086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wC1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "051db4a0-3a25-44fa-aaef-82a7f9d0a7a7",
                                            "FRSS-PI-MDP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wC1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wC1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            ODP3086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wDFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "4b6065f9-dc16-447e-97f7-98681c7dbd5f",
                                            "FRSS-PI-ODP3-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wDFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wDFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP3",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MUP2086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7w_lEAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "004282db-9a62-4159-a815-6e36c9c7707f",
                                            "FRSS-PI-MUP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7w_lEAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7w_lEAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            N5P1086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wCVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "60bf0a4f-b1ff-4e42-b3be-36a474fa7e7c",
                                            "FRSS-PI-N5P1-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wCVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wCVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P1",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            OUP2086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wA1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "d7bffc21-42aa-404d-be64-5d234d73bc46",
                                            "FRSS-PI-OUP2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wA1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wA1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "OUP2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            ODP5086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wCFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "8c8ed396-1b59-4792-8afa-70c5abb51f3e",
                                            "FRSS-PI-ODP5-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wCFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wCFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP5",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MDP1086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wAVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "226367c1-55e6-46f9-a04e-c7f7461a3b2d",
                                            "FRSS-PI-MDP1-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wAVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wAVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP1",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MUP4086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wClIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "9e6bc2c6-2faa-4d5c-b009-bfd425bd9a2b",
                                            "FRSS-PI-MUP4-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wClIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wClIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP4",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            N5P2086,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wB1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "03fda101-d67b-4bdb-9cde-04f2b02a1dfb",
                                            "FRSS-PI-N5P2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wB1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wB1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wEVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "",
                                            "FRSS-PI-N5P2-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wEVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wEVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "ODI2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wD1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDg2LUtHRi1EQUlMWS1BVkc",
                                            "",
                                            "FRSS-PI-ODI1A-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wD1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDg2LUtHRi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wD1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDg2LUtHRi1EQUlMWS1BVkc/value",
                                            "ODI1A",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wEFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "",
                                            "FRSS-PI-OUI3-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wEFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wEFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "OUI3",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            Guid.NewGuid(),
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wDVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "",
                                            "FRSS-PI-N5I1-086-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wDVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wODYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wDVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wODYtS0dGLURBSUxZLUFWRw/value",
                                            "N5I1",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDG1Id,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wGlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "e21c2e85-7eb1-4e46-8cda-371c9b6ab98d",
                                            "FRSS-PI-ODP4-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wGlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wGlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP4",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MUP5096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wE1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "bf7156af-ac3f-4ff7-8bb2-5377e89e014b",
                                            "FRSS-PI-MUP5-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wE1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wE1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP5",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MDP2096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wH1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "c322913d-591c-4931-aa70-76302d2a46ed",
                                            "FRSS-PI-MDP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wH1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wH1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP2",
                                            true,
                                            true,
                                            today
                                        },
                                        //new object[] {
                                        //    ODP3096,
                                        //    "F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                        //    "ad60b0d-2d1a-43dc-acb3-bb4dfaeb265b",
                                        //    "FRSS-PI-ODP3-096-KGF-DAILY-AVG",
                                        //    "",
                                        //    "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                        //    "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw/value",
                                        //    "ODP3",
                                        //    true,
                                        //    true,
                                        //    today
                                        //},
                                        new object[] {
                                            MUP2096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "cb248adf-3cef-4efe-b7ea-3e2c2dc11e8a",
                                            "FRSS-PI-MUP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wIFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wOTYtS0dGLURBSUxZLUFWRw/value"
                                            ,"MUP2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            N5P1096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wHVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "2c0116f0-d80d-48e4-aa91-1937ccc87c02",
                                            "FRSS-PI-N5P1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wHVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wHVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P1",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            OUP2096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wF1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "4245964a-e92a-4a27-8b14-53cdf178691c",
                                            "FRSS-PI-OUP2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wF1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wF1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUP2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            ODP5096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wHFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "9e784e81-b479-4d9a-b7cc-84c0ce389e1e",
                                            "FRSS-PI-ODP5-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wHFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wHFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODP5",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MDP1096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wFVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "bff56eb5-ea0c-4b89-a747-dfadb670d621",
                                            "FRSS-PI-MDP1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wFVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wFVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MDP1",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            MUP4096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wHlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "a4cbda44-0e6f-4ad8-83d3-c1548c6f5051",
                                            "FRSS-PI-MUP4-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wHlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wHlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "MUP4",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            N5P2096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wG1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "90890265-77da-4626-a1a4-2d2ae26b06d5",
                                            "FRSS-PI-N5P2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wG1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wG1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5P2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            N5I1096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wIVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "7ad6653e-c64c-404e-ba35-827d5e6df80d",
                                            "FRSS-PI-N5I1-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wIVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wIVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "N5I1",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            ODI1A096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wI1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "10f5b492-7439-4a66-bd63-c9e1d8281b56",
                                            "FRSS-PI-ODI1A-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wI1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wI1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDk2LUtHRi1EQUlMWS1BVkc/value",
                                            "ODI1A",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            ODI2096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wJVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "465d56fa-53c4-4052-9966-99a3227562bd",
                                            "FRSS-PI-ODI2-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wJVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wJVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "ODI2",
                                            true,
                                            true,
                                            today
                                        },
                                        new object[] {
                                            OUI3096,
                                            "F1DPmcDcljXfiUGwcfDZqjkw7wJFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "63a72578-d10d-46a2-a264-7333738a0a4e",
                                            "FRSS-PI-OUI3-096-KGF-DAILY-AVG",
                                            "",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wJFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw",
                                            "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wJFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wOTYtS0dGLURBSUxZLUFWRw/value",
                                            "OUI3",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      PDG2Id,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wkVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNC0xNTMtREFJTFktQVZH",
                                        "57bbf02f-56b2-4db6-9a8e-aacef25485ac",
                                        "FRSS-FI-ODP4-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wkVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNC0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wkVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNC0xNTMtREFJTFktQVZH/value",
                                        "ODP4",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP5153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wilIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNS0xNTMtREFJTFktQVZH",
                                        "f23f5167-769f-42da-9d04-11579b97155b",
                                        "FRSS-FI-MUP5-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wilIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wilIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNS0xNTMtREFJTFktQVZH/value",
                                        "MUP5",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MDP2153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wllIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMi0xNTMtREFJTFktQVZH",
                                        "558d876d-a4b9-4bb8-8c31-185f93ddc2f2",
                                        "FRSS-FI-MDP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wllIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMi0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wllIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMi0xNTMtREFJTFktQVZH/value",
                                        "MDP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        ODP3153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wl1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQMy0xNTMtREFJTFktQVZH",
                                        "6355391e-5bf4-4e3f-b8a6-2fd884983745",
                                        "FRSS-FI-ODP3-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wl1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQMy0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wl1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQMy0xNTMtREFJTFktQVZH/value",
                                        "ODP3",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP2153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wnVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQMi0xNTMtREFJTFktQVZH",
                                        "7dcb093f-5875-44bd-9ecc-df72b5b8e660",
                                        "FRSS-FI-MUP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wnVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQMi0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wnVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQMi0xNTMtREFJTFktQVZH/value",
                                        "MUP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        N5P1153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wlFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMS0xNTMtREFJTFktQVZH",
                                        "eb3ad3fd-b10a-4d0b-8c27-a7fae1aa276a",
                                        "FRSS-FI-N5P1-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wlFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wlFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMS0xNTMtREFJTFktQVZH/value",
                                        "N5P1",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        OUP2153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wjlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VQMi0xNTMtREFJTFktQVZH",
                                        "b470e13a-b9d0-4abd-a807-b252c36e9928",
                                        "FRSS-FI-OUP2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wjlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VQMi0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wjlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VQMi0xNTMtREFJTFktQVZH/value",
                                        "OUP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        ODP5153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wk1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNS0xNTMtREFJTFktQVZH",
                                        "933c1d50-93ab-44fe-8a03-ea5195362fba",
                                        "FRSS-FI-ODP5-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wk1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wk1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RQNS0xNTMtREFJTFktQVZH/value",
                                        "ODP5",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MDP1153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wjFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMS0xNTMtREFJTFktQVZH",
                                        "79172b3c-8150-4e95-9b88-628b4ef4e6f5",
                                        "FRSS-FI-MDP1-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wjFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wjFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTURQMS0xNTMtREFJTFktQVZH/value",
                                        "MDP1",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP4153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wlVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNC0xNTMtREFJTFktQVZH",
                                        "6b9c6fdd-732d-4771-9323-ab9976c75793",
                                        "FRSS-FI-MUP4-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wlVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNC0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wlVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTVVQNC0xNTMtREFJTFktQVZH/value",
                                        "MUP4",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        N5P2153,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wklIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMi0xNTMtREFJTFktQVZH",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-N5P2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wklIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMi0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wklIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVQMi0xNTMtREFJTFktQVZH/value",
                                        "N5P2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wnFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMi0xNTMtREFJTFktQVZH",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-ODI2-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wnFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMi0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wnFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMi0xNTMtREFJTFktQVZH/value",
                                        "ODI2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wmlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMUEtMTUzLURBSUxZLUFWRw",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-ODI1A-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wmlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMUEtMTUzLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wmlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT0RJMUEtMTUzLURBSUxZLUFWRw/value",
                                        "ODI1A",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wm1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VJMy0xNTMtREFJTFktQVZH",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-OUI3-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wm1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VJMy0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wm1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktT1VJMy0xNTMtREFJTFktQVZH/value",
                                        "OUI3",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wmFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVJMS0xNTMtREFJTFktQVZH",
                                        "31c281ce-c4bd-4aa8-af9e-ea74ce502fb3",
                                        "FRSS-FI-N5I1-153-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wmFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVJMS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wmFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVJMS0xNTMtREFJTFktQVZH/value",
                                        "N5I1",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      VConeId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wLlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wNDMtS0dGLURBSUxZLUFWRw",
                                        "fb4ec996-ac6e-43b0-8fac-1e96a9f2c9cd",
                                        "FRSS-PI-ODP4-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wLlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wLlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNC0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "ODP4",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP5043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wJ1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "0ec7400a-76f6-4d49-9ce4-b2bcfd166215",
                                        "FRSS-PI-MUP5-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wJ1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wJ1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNS0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "MUP5",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MDP2043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wM1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "8cd10f02-eeab-44cf-80f6-1171f17e00e4",
                                        "FRSS-PI-MDP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wM1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wM1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMi0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "MDP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        ODP3043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wNFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wNDMtS0dGLURBSUxZLUFWRw",
                                        "bb8ac32b-4041-4247-ad89-53e00829fdee",
                                        "FRSS-PI-ODP3-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wNFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wNFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQMy0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "ODP3",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP2043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wJlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "124201ee-73dc-4195-81d5-f80f92742f30",
                                        "FRSS-PI-MUP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wJlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wJlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQMi0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "MUP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        N5P1043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wMVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "52779b6f-01d8-478b-9e00-22222d817b0a",
                                        "FRSS-PI-N5P1-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wMVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wMVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMS0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "N5P1",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        OUP2043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wK1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "fa70b578-f528-44ef-b755-c3d069e51e96",
                                        "FRSS-PI-OUP2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wK1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wK1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VQMi0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "OUP2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        ODP5043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wMFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "a255af7d-6026-44c3-abaa-fade7d78377a",
                                        "FRSS-PI-ODP5-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wMFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wMFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RQNS0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "ODP5",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MDP1043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wKVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "bc500ab1-5433-46f3-92a9-e27f4ff9767b",
                                        "FRSS-PI-MDP1-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wKVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wKVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTURQMS0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "MDP1",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        MUP4043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wMlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wNDMtS0dGLURBSUxZLUFWRw",
                                        "2e57314e-0c97-4724-9b80-534e4e577e9f",
                                        "FRSS-PI-MUP4-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wMlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wMlIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTVVQNC0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "MUP4",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        N5P2043,
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wL1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "89ffff76-c574-4ce1-89c8-79dc148b72a6",
                                        "FRSS-PI-N5P2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wL1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wL1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVQMi0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "N5P2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wOVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "",
                                        "FRSS-PI-ODI2-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wOVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wOVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMi0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "ODI2",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wN1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "",
                                        "FRSS-PI-ODI1A-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wN1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDQzLUtHRi1EQUlMWS1BVkc",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wN1IAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT0RJMUEtMDQzLUtHRi1EQUlMWS1BVkc/value",
                                        "ODI1A",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wOFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wNDMtS0dGLURBSUxZLUFWRw",
                                        "",
                                        "FRSS-PI-OUI3-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wOFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wNDMtS0dGLURBSUxZLUFWRw",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wOFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktT1VJMy0wNDMtS0dGLURBSUxZLUFWRw/value",
                                        "OUI3",
                                            true,
                                            true,
                                            today
                                    },
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wNVIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtUEktTjVJMS0wNDMtS0dGLURBSUxZLUFWRw",
                                        "",
                                        "FRSS-PI-N5I1-043-KGF-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wmFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVJMS0xNTMtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wmFIAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSU1MtRkktTjVJMS0xNTMtREFJTFktQVZH/value",
                                        "N5I1",
                                            true,
                                            true,
                                            today
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
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }
                            }
                            else if (elementId == GFL1Id)
                            {
                                var attributesData = new List<object[]>
                                {
                                new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wxAUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAxLVBER1NWT0wuMjBD",
                                        "",
                                        "FRMS-074-FQI-1501-PDGSVOL.20C",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wxAUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAxLVBER1NWT0wuMjBD",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wxAUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAxLVBER1NWT0wuMjBD/value",
                                        "MUP5,MUP2,MDP1",
                                            true,
                                            true,
                                            today
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
                                    var attributeWellName = attribute[7];
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }
                            }
                            else if (elementId == GFL4Id)
                            {
                                var attributesData = new List<object[]>
                                {
                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wyQUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAyLVBER1NWT0wuMjBD",
                                        "",
                                        "FRMS-074-FQI-1502-PDGSVOL.20C",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wyQUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAyLVBER1NWT0wuMjBD",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wyQUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAyLVBER1NWT0wuMjBD/value",
                                        "ODP4,OUP2,ODP5,N5P2",
                                            true,
                                            true,
                                            today
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
                                    var attributeWellName = attribute[7];
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }
                            }
                            else if (elementId == GFL6Id)
                            {
                                var attributesData = new List<object[]>
                                {

                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wzgUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAzLVBER1NWT0wuMjBD",
                                        "",
                                        "FRMS-074-FQI-1503-PDGSVOL.20C",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wzgUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAzLVBER1NWT0wuMjBD",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wzgUAAAQlpGUFNPUElDT0xMRUNUSVZFXEZSTVMtMDc0LUZRSS0xNTAzLVBER1NWT0wuMjBD/value",
                                        "MDP2,ODP3,N5P1,MUP4",
                                            true,
                                            true,
                                            today
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
                                    var attributeWellName = attribute[7];
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
                                      });
                                }
                            }
                            else if (elementId == WFL1Id)
                            {
                                var attributesData = new List<object[]>
                                {

                                    new object[] {
                                        Guid.NewGuid(),
                                        "F1DPmcDcljXfiUGwcfDZqjkw7wxU8AAAQlpGUFNPUElDT0xMRUNUSVZFXEZSVFMtMDYyLUZJLTE1MDEtREFJTFktQVZH",
                                        "",
                                        "FRTS-062-FI-1501-DAILY-AVG",
                                        "",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/attributes/F1DPmcDcljXfiUGwcfDZqjkw7wxU8AAAQlpGUFNPUElDT0xMRUNUSVZFXEZSVFMtMDYyLUZJLTE1MDEtREFJTFktQVZH",
                                        "https://prrjbsrvvm170.petrorio.local/piwebapi/streams/F1DPmcDcljXfiUGwcfDZqjkw7wxU8AAAQlpGUFNPUElDT0xMRUNUSVZFXEZSVFMtMDYyLUZJLTE1MDEtREFJTFktQVZH/value",
                                        "ODI2,ODI1A,OUI3,N5I1",
                                            true,
                                            true,
                                            today
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
                                    var attributeWellName = attribute[7];
                                    var attributeIsActive = attribute[8];
                                    var attributeIsOperating = attribute[9];
                                    var attributeCreatedAt = attribute[10];

                                    migrationBuilder.InsertData(
                                      table: "PI.Attributes",
                                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ValueRoute", "ElementId", "WellName", "IsActive", "IsOperating", "CreatedAt" },
                                      values: new object[] {
                                      attributeId,
                                      attributeWebId,
                                      attributePIId,
                                      attributeName,
                                      attributeDescription,
                                      attributeSelfRoute,
                                      attributeElementsRoute,
                                      SSPCVId,
                                      attributeWellName,
                                      attributeIsActive,
                                      attributeIsOperating,
                                      attributeCreatedAt,
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
