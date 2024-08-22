namespace Skyline.DataMiner.ServiceNow.Connector.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Skyline.DataMiner.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Common;
    using Newtonsoft.Json;
    using static Skyline.DataMiner.ServiceNow.Connector.Common.Connector;

    /// <summary>
    /// Data structure used to perform mappings between ServiceNow CIs and its properties and the respective Dataminer counterparts.
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// Describes the different options that can be used to build a CI unique ID.
        /// </summary>
        public enum NamingFormat
        {
            // TODO: Check which prefix better serves the purpose of protecting ID uniqueness (Element DMA ID might cause trouble if swarming is used in the future)
            // Naming format always includes parent element name to avoid duplicating unique IDs (for instance in case there are duplicate elements)
            Name,
            Name_Label,
            Label,
            Label_Name,
            Custom,
        }

        private static Dictionary<string, ConnectorMapping> classMappingsByConnector;

        public static Dictionary<string, ConnectorMapping> Mappings
        {
            get
            {
                if (classMappingsByConnector != null)
                {
                    return classMappingsByConnector;
                }

                classMappingsByConnector = new Dictionary<string, ConnectorMapping>
                {
                    // TODO: Include all supported Connector/CI Types mappings here
                    {
                        "iDirect Platform", new ConnectorMapping(
                            new List<ClassMapping>
                            {
                                new ClassMapping
                                {
                                    Class = "Evolution NMS",
                                    TargetTable = "u_cmdb_ci_appl_nms_evolution",
                                    IsParent = true,
                                    NamingDetails = new NamingDetails(NamingFormat.Name, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        //  ...
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Remote",
                                    TargetTable = "u_cmdb_ci_modem_evolution_remote",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Custom, new List<string> { "u_label", "customer_id" }),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 6, true),
                                                new ClassAttribute("u_network_id", 9, false),
                                                new ClassAttribute("u_network_name", 10, true),
                                                new ClassAttribute("u_inroute_group_id", 11, false),
                                                new ClassAttribute("u_inroute_group", 12, false),
                                                new ClassAttribute("u_customer_id", 13, false),
                                                new ClassAttribute("u_active_sw_version", 14, false),
                                                new ClassAttribute("u_hw_type", 15, false),
                                                new ClassAttribute("u_protocol_processor", 16, false),
                                                new ClassAttribute("serial_number", 17, false),
                                                new ClassAttribute("u_nms_name", 18, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Linecard",
                                    TargetTable = "u_cmdb_ci_modem_evolution_linecard",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Name, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            400,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 4, true),
                                                new ClassAttribute("u_inroute_group", 8, false),
                                                new ClassAttribute("u_customer_id", 9, false),
                                                new ClassAttribute("u_active_sw_version", 10, false ),
                                                new ClassAttribute("u_hw_type", 11, false),
                                                new ClassAttribute("u_protocol_processor", 12, false),
                                                new ClassAttribute("serial_number", 13, false),
                                                new ClassAttribute("u_chassis_id", 14, false),
                                                new ClassAttribute("u_chassis_slot_number", 15, false),
                                                new ClassAttribute("u_network_id", 16, false),
                                                new ClassAttribute("u_nms_name", 17, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Network",
                                    TargetTable = "u_cmdb_ci_group_evolution_network",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Name_Label, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            600,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 3, true),
                                                new ClassAttribute("u_teleport_id", 6, false),
                                                new ClassAttribute("u_protocol_processor", 7, false),
                                                new ClassAttribute("u_nms_name", 8, false),
                                                //TODO: Include Network PP Name ???
                                            }
                                        },
                                        {
                                            6000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_network_pp_name", 30, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Chassis",
                                    TargetTable = "cmdb_ci_chassis",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Custom, new List<string> { "u_label", "u_serial_number" }),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            1500,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_serial_number", 2, false),
                                                new ClassAttribute("u_status", 3, true),
                                                new ClassAttribute("u_nms_ip", 4, false),
                                                new ClassAttribute("u_nms_name", 5, false),
                                            }
                                        },
                                        {
                                            1700,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("fk", 1, false),
                                                new ClassAttribute("u_linecard", 4, false),
                                                new ClassAttribute("u_redundancy_linecard", 8, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Inroute Group",
                                    TargetTable = "u_cmdb_ci_evolution_inroute_group",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Name_Label, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            7400,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Protocol Processor",
                                    TargetTable = "u_cmdb_ci_appl_evolution_pp",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Custom, new List<string>()),
                                    //TODO: Add suport for naming based on External CIs
                                    //NamingDetails = new NamingDetails(NamingFormat.Custom, new List<string> { "u_label", "u_network_pp_name" }),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            6300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Protocol Processor Blade",
                                    TargetTable = "u_cmdb_ci_evolution_protocol_processor_blade",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Name_Label, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            6300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                                new ClassAttribute("u_tunnel_address", 3, false),
                                                new ClassAttribute("u_tunnel_subnet", 4, false),
                                                new ClassAttribute("u_upstream_address", 5, false),
                                                new ClassAttribute("u_upstream_subnet", 6, false),
                                            }
                                        },
                                    },
                                },
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Teleport",
                                //    TargetTable = "u_cmdb_ci_group_evolution_teleport",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Application",
                                //    TargetTable = "u_cmdb_ci_appl_evolution",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Encapsulator",
                                //    TargetTable = "u_cmdb_ci_appl_evolution_encapsulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Processing Node",
                                //    TargetTable = "u_cmdb_ci_appl_evolution_processing_node",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Evolution Remote", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Managed by::Manages", false),
                                new Relationship("Evolution Linecard", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Managed by::Manages", false),
                                new Relationship("Evolution Network", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Managed by::Manages", false),
                                new Relationship("Evolution Inroute Group", "Evolution Remote", "u_inroute_group", String.Empty, String.Empty, String.Empty,  String.Empty, "Connected by::Connects", true),
                                new Relationship("Evolution Network", "Evolution Remote", "u_network_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Receives data from::Sends data to", true),
                                new Relationship("Evolution Network", "Evolution Linecard", "u_network_id", String.Empty, String.Empty, String.Empty, String.Empty,  "Depends on::Used by", true),
                                new Relationship("Evolution Linecard", "Evolution Chassis", "u_chassis_id", String.Empty, String.Empty, String.Empty, String.Empty,  "Located in::Houses", false),
                                new Relationship("Evolution Network", "Evolution Protocol Processor", "u_protocol_processor", String.Empty, String.Empty, String.Empty, String.Empty,  "Depends on::Used by", false),
                                //TODO: Change relationship mapping as following mapping requires data from different CI Class 
                                new Relationship("Evolution Linecard", "Evolution Linecard", "u_label", "u_linecard", "u_redundancy_linecard", String.Empty, "Evolution Chassis", "DR provided by::Provides DR for", false),
                            })
                    },
                    {
                        "Newtec Dialog Time Series Database", new ConnectorMapping(
                            new List<ClassMapping>
                            {
                                new ClassMapping
                                {
                                    Class = "Dialog NMS",
                                    TargetTable = "u_cmdb_ci_appl_nms_dialog",
                                    IsParent = true,
                                    NamingDetails = new NamingDetails(NamingFormat.Name, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Dialog Remote",
                                    TargetTable = "u_cmdb_ci_modem_dialog_remote",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Label, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            12000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 7, false),
                                                new ClassAttribute("u_modem_name", 1, false),
                                                new ClassAttribute("u_modem_type", 2, false),
                                                new ClassAttribute("u_return_technology", 3, false),
                                                new ClassAttribute("u_mac_address", 4, false),
                                                new ClassAttribute("u_monitoring_type", 5, false),
                                                new ClassAttribute("u_network_name", 6, false),
                                                new ClassAttribute("u_network_config", 8, false),
                                                new ClassAttribute("u_sw_version", 9, false),
                                                new ClassAttribute("u_last_network_config", 10, false),
                                                new ClassAttribute("u_status", 149, true),
                                                new ClassAttribute("u_nms_name", -1, false),
                                            }
                                        },
                                        {
                                            21000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_beam_state", 2, true),
                                                new ClassAttribute("u_active_beam", 3, true),
                                                new ClassAttribute("u_switching_beam", 4, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Dialog Satellite Network",
                                    TargetTable = "u_cmdb_ci_dialog_satellite_network",
                                    IsParent = false,
                                    NamingDetails = new NamingDetails(NamingFormat.Name_Label, new List<string>()),
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            15000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_beam_id", 2, false),
                                                new ClassAttribute("u_beam_signaled_name", 3, false),
                                                new ClassAttribute("u_beam_orbital_position", 4, false),
                                                new ClassAttribute("u_beam_east_west_flag", 5, false),
                                            }
                                        },
                                    },
                                },
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Application",
                                //    TargetTable = "u_cmdb_ci_dialog_application",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Hub",
                                //    TargetTable = "u_cmdb_ci_dialog_hub",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Modulator",
                                //    TargetTable = "u_cmdb_ci_dialog_modulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Demodulator",
                                //    TargetTable = "u_cmdb_ci_dialog_demodulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Dialog Remote", "Dialog NMS", "u_nms_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Managed by::Manages", false),
                                new Relationship("Dialog Satellite Network", "Dialog NMS", "u_nms_name", String.Empty, String.Empty, String.Empty, String.Empty,  "Managed by::Manages", false),
                                new Relationship("Dialog Satellite Network", "Dialog Remote", "u_active_beam", String.Empty, String.Empty, String.Empty, String.Empty,  "Receives data from::Sends data to", true),
                            })
                    },
                };

                return classMappingsByConnector;
            }
        }

        /// <summary>
        /// Get elements running supported connectors as defined by the in the Connector class mappings.
        /// </summary>
        /// <param name="engine"></param>
        /// <returns>list of elements running supported connectors.</returns>
        public static List<Element> GetSupportedDmsElements(IEngine engine)
        {
            var elements = new List<Element>();

            var supportedConnectors = Mappings.Keys.ToList();

            foreach (var connector in supportedConnectors)
            {
                var foundElements = engine.FindElementsByProtocol(connector);

                if (foundElements.Length > 0)
                {
                    elements.AddRange(foundElements);
                }
            }

            return elements;
        }

        /// <summary>
        /// Method used to retrieve the protocol attribute values to be pushed into the ServiceNow integration.
        /// </summary>
        /// <param name="protocolName"></param>
        /// <returns>List of parameter updates.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<ParameterDetails> GetPushParameterDetailsByConnector(string protocolName)
        {
            if (Mappings.ContainsKey(protocolName))
            {
                var parameterUpdates = new List<ParameterDetails>();

                var connectorMapping = Mappings[protocolName];

                foreach (var classMapping in connectorMapping.ClassMappings)
                {
                    var classAttributesByTablePID = classMapping.AttributesByTableID;

                    foreach (var attributeKvp in classAttributesByTablePID)
                    {
                        var pushAttributes = attributeKvp.Value.Where(attribute => attribute.HasPushEvent).ToList();

                        if (pushAttributes.Count == 0) continue;

                        foreach (var namingAttribute in classMapping.NamingDetails.RequiredProperties)
                        {
                            var attributeToAdd = pushAttributes.FirstOrDefault(x => x.Name.Equals(namingAttribute));

                            if (attributeToAdd != null)
                            {
                                // Add class attributes that will be used to retrieve instances Unique IDs
                                pushAttributes.Add(attributeToAdd);
                            }
                        }

                        var parameterDetails = pushAttributes
                            .Select(attribute => new ParameterDetails(attribute.Name, classMapping.Class, new KeyValuePair<int, int>(attributeKvp.Key, attribute.ColumnIdx)));

                        parameterUpdates.AddRange(parameterDetails);
                    }
                }

                return parameterUpdates;
            }
            else
            {
                throw new ArgumentException($"Protocol name '{protocolName}' could not be found in connector mappings.");
            }
        }
    }

    /// <summary>
    /// Data structure that details the mappings that can be performed from any supported connector.
    /// </summary>
    public class ConnectorMapping
    {
        /// <summary>
        /// Class mappings retrieved from the supported connector.
        /// </summary>
        public List<ClassMapping> ClassMappings { get; set; }

        /// <summary>
        /// Relationship details to build from the supported connector.
        /// </summary>
        public List<Relationship> Relationships { get; set; }

        /// <summary>
        /// ConnectorMapping class contructor.
        /// </summary>
        /// <param name="classMappings"></param>
        /// <param name="relationships"></param>
        public ConnectorMapping(List<ClassMapping> classMappings, List<Relationship> relationships)
        {
            ClassMappings = classMappings;
            Relationships = relationships;
        }
    }

    /// <summary>
    /// Data structure that details for each class mapping.
    /// </summary>
    public class ClassMapping
    {
        private Dictionary<string, Func<Engine, List<Property>, string, string>> ciUniqueIdFunctionMapper;

        public Dictionary<string, Func<Engine, List<Property>, string, string>> CiUniqueIdFunctionMapper
        {
            get
            {
                if (ciUniqueIdFunctionMapper != null)
                {
                    return ciUniqueIdFunctionMapper;
                }

                ciUniqueIdFunctionMapper = new Dictionary<string, Func<Engine, List<Property>, string, string>>
                {
                    //  TODO: Add methods used to build CIs using custom methods
                    { "Evolution Remote", GetEvolutionRemoteUniqueID },
                    { "Evolution Chassis", GetEvolutionChassisUniqueID },
                    { "Evolution Protocol Processor", GetEvolutionProtocolProcessorUniqueID },
                };

                return ciUniqueIdFunctionMapper;
            }
        }

        /// <summary>
        /// Class name.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// ServiceNow target table for the class.
        /// </summary>
        public string TargetTable { get; set; }

        /// <summary>
        /// Indicates if the class has a parent relationship to other connector classes.
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Contains the details necessary to build unique IDs for CIs of a given class.
        /// </summary>
        public NamingDetails NamingDetails { get; set; }

        /// <summary>
        /// Specifies the details necessary to retrieve the mapped class attributes from the corresponding Dataminer elements.
        /// </summary>
        public Dictionary<int, List<ClassAttribute>> AttributesByTableID { get; set; }

        /// <summary>
        /// Method used to retrieve parsed property values organized by instance unique ID.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="element"></param>
        /// <returns>Dictionary containg a list of properties objects by unique id.</returns>
        public Dictionary<string, List<Property>> GetPropertiesByCiUniqueID(IEngine engine, Element element)
        {
            var propertiesByPK = new Dictionary<string, List<Property>>();
            var propertiesByFK = new Dictionary<string, Dictionary<string, List<string>>>();

            var rowsByTableID = AttributesByTableID.ToDictionary(x => x.Key, x => GetClassCiRows(engine, element, x.Key));

            foreach (var rowByTableKvp in rowsByTableID)
            {
                if (rowsByTableID.Values.Count == 0) continue;

                ParseRowProperties(propertiesByPK, propertiesByFK, rowByTableKvp);
            }

            var propertiesByUniqueID = new Dictionary<string, List<Property>>();

            string elementDmsID = element.DmaId + "/" + element.ElementId;

            foreach (var item in propertiesByPK)
            {
                var uniqueID = GetCiRowUniqueID(engine, item.Key, item.Value, elementDmsID);

                if (String.IsNullOrWhiteSpace(uniqueID))
                {
                    string message = "GetPropertiesByCiUniqueID| Could not retrieve unique ID for key " + item.Key + " and element '" + element.ElementName + "'.";
                    engine.GenerateInformation(message);
                    continue;
                }

                var propertyList = item.Value;

                if (propertiesByUniqueID.ContainsKey(uniqueID)) continue;

                propertyList.Add(new Property("name", Class, uniqueID));
                propertyList.Add(new Property("operational_status", Class, "Operational"));

                propertiesByUniqueID.Add(uniqueID, propertyList);

                if (!propertiesByFK.ContainsKey(item.Key)) continue;

                var propertiesValuesByAttributeName = propertiesByFK[item.Key];

                propertyList = propertiesValuesByAttributeName.Select(kvp => new Property(kvp.Key, Class, String.Join(";", kvp.Value))).ToList();

                engine.GenerateInformation("GetPropertiesByCiUniqueID| Property List:\n\n" + JsonConvert.SerializeObject(propertyList) + "\n\n");

                if (!propertiesByUniqueID.ContainsKey(uniqueID))
                {
                    propertiesByUniqueID.Add(uniqueID, new List<Property>());
                }

                propertiesByUniqueID[uniqueID].AddRange(propertyList);
            }

            return propertiesByUniqueID;
        }

        /// <summary>
        /// Method used to retrieve a list of rows containg data related to CIs of a given Class.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="element"></param>
        /// <param name="tablePid"></param>
        /// <returns>Dictionary containg a list of properties objects by unique id.</returns>
        private static List<object[]> GetClassCiRows(IEngine engine, Element element, int tablePid)
        {
            try
            {
                var dms = engine.GetDms();

                var dmsElement = dms.GetElement(new DmsElementId(element.DmaId, element.ElementId));

                var dmsTable = dmsElement.GetTable(tablePid);

                var table = dmsTable.GetRows();

                return GetRowList(table);
            }
            catch (ElementStoppedException)
            {
                engine.GenerateInformation("GetClassCiRows| Element '" + element.Name + "' is stopped.");
                return new List<object[]>();
            }
            catch (Exception ex)
            {
                engine.Log("GetClassCiRows| Exception:\n\n" + ex + "\n\n");
                return new List<object[]>();
            }
        }

        private static List<object[]> GetRowList(object[][] table)
        {
            var rowList = new List<object[]>();

            for (int i = 0; i < table.Length; i++)
            {
                rowList.Add(table[i]);
            }

            return rowList;
        }

        private void ParseRowProperties(Dictionary<string, List<Property>> propertiesByPK, Dictionary<string, Dictionary<string, List<string>>> propertiesByFK, KeyValuePair<int, List<object[]>> rowByTableKvp)
        {
            int tablePid = rowByTableKvp.Key;
            var rows = rowByTableKvp.Value;

            var primaryKeyAttribute = AttributesByTableID[tablePid].FirstOrDefault(x => x.Name.Equals("pk"));
            var foreignKeyAttribute = AttributesByTableID[tablePid].FirstOrDefault(x => x.Name.Equals("fk"));

            foreach (var row in rows)
            {
                if (primaryKeyAttribute != null)
                {
                    ParsePropertiesByRowPK(propertiesByPK, tablePid, primaryKeyAttribute, row);
                }

                if (foreignKeyAttribute != null)
                {
                    var fk = Convert.ToString(row[foreignKeyAttribute.ColumnIdx]);

                    if (String.IsNullOrWhiteSpace(fk) || fk.Equals("-1") || fk.Equals("NA")) continue;

                    if (!propertiesByFK.ContainsKey(fk))
                    {
                        propertiesByFK.Add(fk, new Dictionary<string, List<string>>());
                    }

                    ParsePropertiesByRowFK(propertiesByFK[fk], row, tablePid);
                }
            }
        }

        private void ParsePropertiesByRowPK(Dictionary<string, List<Property>> propertiesByPK, int tablePid, ClassAttribute primaryKeyAttribute, object[] row)
        {
            var pk = Convert.ToString(row[primaryKeyAttribute.ColumnIdx]);

            if (String.IsNullOrWhiteSpace(pk) || pk.Equals("-1") || pk.Equals("NA")) return;

            if (!propertiesByPK.ContainsKey(pk))
            {
                propertiesByPK.Add(pk, new List<Property>());
            }

            foreach (var classAttribute in AttributesByTableID[tablePid])
            {
                if (classAttribute.Name.Equals("pk")) continue;

                propertiesByPK[pk].Add(new Property(classAttribute.Name, Class, Convert.ToString(row[classAttribute.ColumnIdx])));
            }
        }

        private void ParsePropertiesByRowFK(Dictionary<string, List<string>> propertiesValuesByAttributeName, object[] row, int tablePid)
        {
            foreach (var classAttribute in AttributesByTableID[tablePid])
            {
                if (classAttribute.Name.Equals("fk")) continue;

                if (propertiesValuesByAttributeName.ContainsKey(classAttribute.Name))
                {
                    propertiesValuesByAttributeName[classAttribute.Name].Add(Convert.ToString(row[classAttribute.ColumnIdx]));
                }
                else
                {
                    propertiesValuesByAttributeName.Add(classAttribute.Name, new List<string> { Convert.ToString(row[classAttribute.ColumnIdx]) });
                }
            }
        }

        private string GetCiRowUniqueID(IEngine engine, string pk, List<Property> properties, string parentElementName)
        {
            switch (NamingDetails.Format)
            {
                case NamingFormat.Name:
                    {
                        return parentElementName + "_" + pk;
                    }

                case NamingFormat.Name_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + pk + "_" + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label_Name:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + labelProperty.Value + "_" + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        return parentElementName + "_" + CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
                    }

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Method used to retrieve supported connector table values that should be sent into the update solution push request.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="pushParameters"></param>
        /// <param name="parameterIndices"></param>
        /// <param name="rowsByPK"></param>
        public static void FillTableParameterValues(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<ParameterDetails> pushParameters, List<uint> parameterIndices, Dictionary<string, object[]> rowsByPK)
        {
            foreach (var rowKvp in rowsByPK)
            {
                if (!parameterValuesByPK.ContainsKey(rowKvp.Key))
                {
                    parameterValuesByPK.Add(rowKvp.Key, new List<ParameterDetails>());
                }

                foreach (var parameter in pushParameters)
                {
                    var searchedParameter = parameterValuesByPK[rowKvp.Key].FirstOrDefault(p => p.AttributeName.Equals(parameter.AttributeName));

                    if (searchedParameter == null)
                    {
                        searchedParameter = new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.ParameterIdxByPid);

                        parameterValuesByPK[rowKvp.Key].Add(searchedParameter);
                    }

                    if (parameter.ParameterIdxByPid.Value == -1) continue;

                    int rowIdx = parameterIndices.FindIndex(x => x == (uint)parameter.ParameterIdxByPid.Value);

                    if (rowIdx == -1) continue;

                    searchedParameter.CurrentValue = Convert.ToString(rowKvp.Value[rowIdx]);
                }
            }
        }

        /// <summary>
        /// Method used to remove parameter values related to deprecated PKs.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="currentPKs"></param>
        public static void RemoveDeprecatedParameterValueKeys(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<string> currentPKs)
        {
            var deprecatedPKs = parameterValuesByPK.Keys.Where(pk => !currentPKs.Contains(pk)).ToList();

            foreach (var deprecatedPK in deprecatedPKs)
            {
                parameterValuesByPK.Remove(deprecatedPK);
            }
        }

        /// <summary>
        /// Method used to remove parameter values related to deprecated attributes.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="pushParameters"></param>
        public static void RemoveDeprecatedAttributes(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<ParameterDetails> pushParameters)
        {
            var pushParameterNames = pushParameters.Select(p => p.AttributeName).ToList();

            foreach (var parameterValuesKvp in parameterValuesByPK)
            {
                parameterValuesKvp.Value.RemoveAll(p => !pushParameterNames.Contains(p.AttributeName));
            }
        }

        /// <summary>
        /// Method used to retrieve monitored parameter updates to send via push request.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="parameterUpdates"></param>
        /// <returns>Boolean indicating if there are parameter updates to send.</returns>
        public static bool TryGetParameterUpdatesToSend(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, out Dictionary<string, List<ParameterDetails>> parameterUpdates)
        {
            parameterUpdates = new Dictionary<string, List<ParameterDetails>>();

            foreach (var parameterDetailsKvp in parameterValuesByPK)
            {
                foreach (var parameter in parameterDetailsKvp.Value)
                {
                    if (!parameter.CurrentValue.Equals(parameter.PreviousValue))
                    {
                        if (parameterUpdates.ContainsKey(parameterDetailsKvp.Key))
                        {
                            parameterUpdates[parameterDetailsKvp.Key]
                                .Add(new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue));
                        }
                        else
                        {
                            parameterUpdates.Add(parameterDetailsKvp.Key, new List<ParameterDetails>
                            {
                                new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue),
                            });
                        }
                    }

                    //Clear parameter values for next polling cycle
                    parameter.PreviousValue = parameter.CurrentValue;
                    parameter.CurrentValue = String.Empty;
                }
            }

            return parameterUpdates.Count > 0;
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="parentElementName"></param>
        /// <param name="pk"></param>
        /// <param name="classNamingFormat"></param>
        /// <param name="parameterDetails"></param>
        /// <returns>Instance unique ID.</returns>
        public string GetInstanceUniqueID(IEngine engine, string parentElementName, string pk, NamingFormat classNamingFormat, List<ParameterDetails> parameterDetails)
        {
            switch (classNamingFormat)
            {
                case NamingFormat.Name:
                    {
                        return parentElementName + "_" + pk;
                    }

                case NamingFormat.Name_Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + pk + "_" + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label_Name:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + labelAttribute.CurrentValue + "_" + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        var properties = parameterDetails.Select(p => new Property(p.AttributeName, Class, p.CurrentValue)).ToList();

                        return parentElementName + "_" + CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
                    }

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given Evolution Remote instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="properties"></param>
        /// <param name="pk"></param>
        /// <returns>Remote instance unique ID.</returns>
        public static string GetEvolutionRemoteUniqueID(Engine engine, List<Property> properties, string pk)
        {
            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            var customerIdProperty = properties.FirstOrDefault(x => x.Name.Equals("u_customer_id"));

            return labelProperty != null && customerIdProperty != null ? customerIdProperty.Value + "_" + pk + "_" + labelProperty.Value : String.Empty;
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given Evolution Chassis instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="properties"></param>
        /// <param name="pk"></param>
        /// <returns>Remote instance unique ID.</returns>
        public static string GetEvolutionChassisUniqueID(Engine engine, List<Property> properties, string pk)
        {
            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            var serialNumberProperty = properties.FirstOrDefault(x => x.Name.Equals("u_serial_number"));

            return labelProperty != null && serialNumberProperty != null ? serialNumberProperty.Value + "_" + pk + "_" + labelProperty.Value : String.Empty;
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given Evolution Protocol Processor instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="properties"></param>
        /// <param name="pk"></param>
        /// <returns>Remote instance unique ID.</returns>
        public static string GetEvolutionProtocolProcessorUniqueID(Engine engine, List<Property> properties, string pk)
        {
            //TODO: Change the method to use the configured PP Name

            string uniqueID = "ppstack " + pk.Split('_').First();

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty != null)
            {
                labelProperty.Value = uniqueID;
            }

            return uniqueID;
        }
    }

    public class ClassAttribute
    {
        public string Name { get; set; }

        public int ColumnIdx { get; set; }

        public bool HasPushEvent { get; set; }

        /// <summary>
        /// ClassAttribute class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="columnIdx"></param>
        /// <param name="hasPushEvent"></param>
        public ClassAttribute(string name, int columnIdx, bool hasPushEvent)
        {
            Name = name;
            ColumnIdx = columnIdx;
            HasPushEvent = hasPushEvent;
        }
    }

    public class Property
    {
        public enum State
        {
            Unknown = -99,
            Deactivated = 0,
            Active,
            Removed,
            Decomissioned,
        }

        public string Name { get; set; }

        public string Class { get; set; }

        public string Value { get; set; }

        /// <summary>
        /// Property class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="className"></param>
        /// <param name="value"></param>
        public Property(string name, string className, string value)
        {
            Name = name;
            Class = className;
            Value = GetPropertyValueByName(value);
        }

        private string GetPropertyValueByName(string value)
        {
            switch (Name)
            {
                case "u_status":
                    return GetInstanceStatus(value);

                default:
                    return value;
            }
        }

        public static string GetInstanceStatus(string status)
        {
            if (String.IsNullOrWhiteSpace(status) || status.Equals("True"))
            {
                return State.Active.ToString();
            }

            return !IsNumerical(status) ? status : GetEnumValue(status).ToString();
        }

        private static State GetEnumValue(string input)
        {
            int index;
            if (int.TryParse(input, out index) && Enum.IsDefined(typeof(State), index))
            {
                return (State)index;
            }
            else
            {
                return State.Unknown;
            }
        }

        private static bool IsNumerical(string input)
        {
            // Attempt to parse the input string as a double
            if (Int32.TryParse(input.Trim(), out _))
            {
                return true; // Input is numerical
            }
            else
            {
                return false; // Input is not numerical
            }
        }
    }

    /// <summary>
    /// Contains the relationship details.
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Relationship name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Class of the relationship parent CI.
        /// </summary>
        public string ParentClass { get; set; }

        /// <summary>
        /// Class of the relationship child CI.
        /// </summary>
        public string ChildClass { get; set; }

        /// <summary>
        /// Describes the property requirements necessary to build a given relationship.
        /// </summary>
        public PropertyLink PropertyLink { get; set; }

        /// <summary>
        /// Relationship type/description.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Indicates if the relationship mapping should be performed from property values contained in the parent CI.
        /// </summary>
        public bool IsMappedFromParent { get; set; }

        /// <summary>
        /// Relationship class constructor.
        /// </summary>
        /// <param name="childClass"></param>
        /// <param name="parentClass"></param>
        /// <param name="linkProperty"></param>
        /// <param name="childExternalProperty"></param>
        /// <param name="parentExternalProperty"></param>
        /// <param name="childExternalClass"></param>
        /// <param name="parentExternalClass"></param>
        /// <param name="type"></param>
        /// <param name="isMappedFromParent"></param>
        public Relationship(string childClass, string parentClass, string linkProperty, string childExternalProperty, string parentExternalProperty, string childExternalClass, string parentExternalClass, string type, bool isMappedFromParent)
        {
            Name = childClass + "/" + type + "/" + parentClass;
            ParentClass = parentClass;
            ChildClass = childClass;
            PropertyLink = new PropertyLink(linkProperty, childExternalProperty, parentExternalProperty, childExternalClass, parentExternalClass);
            Type = type;
            IsMappedFromParent = isMappedFromParent;
        }
    }

    /// <summary>
    /// Describes how a given property is used to link CIs that have a certain relationship.
    /// </summary>
    public class PropertyLink
    {
        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contains the details of a property that belongs to an external child CI, but needs to be used to build a given relationship.
        /// </summary>
        public Property ChildExternalProperty { get; set; }

        /// <summary>
        /// Contains the details of a property that belongs to an external parent CI, but needs to be used to build a given relationship.
        /// </summary>
        public Property ParentExternalProperty { get; set; }

        /// <summary>
        /// PropertyLink class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="childExternalProperty"></param>
        /// <param name="parentExternalProperty"></param>
        /// <param name="childExternalClass"></param>
        /// <param name="parentExternalClass"></param>
        public PropertyLink(string name, string childExternalProperty, string parentExternalProperty, string childExternalClass, string parentExternalClass)
        {
            Name = name;
            ChildExternalProperty = new Property(childExternalProperty, childExternalClass, String.Empty);
            ParentExternalProperty = new Property(parentExternalProperty, parentExternalClass, String.Empty);
        }
    }

    /// <summary>
    /// Contains the details of a monitored parameter.
    /// </summary>
    public class ParameterDetails
    {
        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// Class of the attribute
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// KeyValuePair containing info on how to retrieve a given parameter from a supported connector.
        /// </summary>
        public KeyValuePair<int, int> ParameterIdxByPid { get; set; }

        /// <summary>
        /// Current value of the monitored parameter.
        /// </summary>
        public string CurrentValue { get; set; }

        /// <summary>
        /// Previous value of the monitored parameter.
        /// </summary>
        public string PreviousValue { get; set; }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="paramIdxByPid"></param>
        /// <param name="currentValue"></param>
        /// <param name="previousValue"></param>
        [JsonConstructor]
        public ParameterDetails(string attributeName, string className, KeyValuePair<int, int> paramIdxByPid, string currentValue, string previousValue)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = currentValue;
            PreviousValue = previousValue;
        }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="paramIdxByPid"></param>
        public ParameterDetails(string attributeName, string className, KeyValuePair<int, int> paramIdxByPid)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = String.Empty;
            PreviousValue = String.Empty;
        }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="currentValue"></param>
        public ParameterDetails(string attributeName, string className, string currentValue)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = new KeyValuePair<int, int>();
            CurrentValue = currentValue;
            PreviousValue = String.Empty;
        }
    }

    /// <summary>
    /// Contains the details necessary to build a CI unique ID.
    /// </summary>
    public class NamingDetails
    {
        /// <summary>
        /// Unique ID naming format.
        /// </summary>
        public NamingFormat Format { get; set; }

        /// <summary>
        /// List of properties that are required to build the unique ID of a given CI.
        /// </summary>
        public List<string> RequiredProperties { get; set; }

        /// <summary>
        /// NamingDetails class constructor.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="requiredProperties"></param>
        public NamingDetails(NamingFormat format, List<string> requiredProperties)
        {
            Format = format;
            RequiredProperties = requiredProperties;
        }
    }
}
