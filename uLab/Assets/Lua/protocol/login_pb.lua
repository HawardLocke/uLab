-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
module('login_pb')


local EXTRAINFO = protobuf.Descriptor();
local EXTRAINFO_NUMBER_FIELD = protobuf.FieldDescriptor();
local LOGIN = protobuf.Descriptor();
local LOGIN_NAME_FIELD = protobuf.FieldDescriptor();
local LOGIN_PASSWORD_FIELD = protobuf.FieldDescriptor();
local LOGIN_INFOS_FIELD = protobuf.FieldDescriptor();
local TESTMSG1 = protobuf.Descriptor();
local TESTMSG1_TEXT_FIELD = protobuf.FieldDescriptor();
local TESTMSG1_NUMBER_FIELD = protobuf.FieldDescriptor();
local TESTMSG1_BIGNUMBER_FIELD = protobuf.FieldDescriptor();
local TESTMSG2 = protobuf.Descriptor();
local TESTMSG2_TEXT_FIELD = protobuf.FieldDescriptor();
local TESTMSG2_NUMBER_FIELD = protobuf.FieldDescriptor();
local TESTMSG2_BIGNUMBER_FIELD = protobuf.FieldDescriptor();
local TESTMSG3 = protobuf.Descriptor();
local TESTMSG3_TEXT_FIELD = protobuf.FieldDescriptor();
local TESTMSG3_NUMBER_FIELD = protobuf.FieldDescriptor();
local TESTMSG3_BIGNUMBER_FIELD = protobuf.FieldDescriptor();

EXTRAINFO_NUMBER_FIELD.name = "number"
EXTRAINFO_NUMBER_FIELD.full_name = ".LiteServer.ExtraInfo.number"
EXTRAINFO_NUMBER_FIELD.number = 1
EXTRAINFO_NUMBER_FIELD.index = 0
EXTRAINFO_NUMBER_FIELD.label = 1
EXTRAINFO_NUMBER_FIELD.has_default_value = false
EXTRAINFO_NUMBER_FIELD.default_value = 0
EXTRAINFO_NUMBER_FIELD.type = 5
EXTRAINFO_NUMBER_FIELD.cpp_type = 1

EXTRAINFO.name = "ExtraInfo"
EXTRAINFO.full_name = ".LiteServer.ExtraInfo"
EXTRAINFO.nested_types = {}
EXTRAINFO.enum_types = {}
EXTRAINFO.fields = {EXTRAINFO_NUMBER_FIELD}
EXTRAINFO.is_extendable = false
EXTRAINFO.extensions = {}
LOGIN_NAME_FIELD.name = "name"
LOGIN_NAME_FIELD.full_name = ".LiteServer.Login.name"
LOGIN_NAME_FIELD.number = 1
LOGIN_NAME_FIELD.index = 0
LOGIN_NAME_FIELD.label = 1
LOGIN_NAME_FIELD.has_default_value = false
LOGIN_NAME_FIELD.default_value = ""
LOGIN_NAME_FIELD.type = 9
LOGIN_NAME_FIELD.cpp_type = 9

LOGIN_PASSWORD_FIELD.name = "password"
LOGIN_PASSWORD_FIELD.full_name = ".LiteServer.Login.password"
LOGIN_PASSWORD_FIELD.number = 2
LOGIN_PASSWORD_FIELD.index = 1
LOGIN_PASSWORD_FIELD.label = 1
LOGIN_PASSWORD_FIELD.has_default_value = false
LOGIN_PASSWORD_FIELD.default_value = ""
LOGIN_PASSWORD_FIELD.type = 9
LOGIN_PASSWORD_FIELD.cpp_type = 9

LOGIN_INFOS_FIELD.name = "infos"
LOGIN_INFOS_FIELD.full_name = ".LiteServer.Login.infos"
LOGIN_INFOS_FIELD.number = 3
LOGIN_INFOS_FIELD.index = 2
LOGIN_INFOS_FIELD.label = 3
LOGIN_INFOS_FIELD.has_default_value = false
LOGIN_INFOS_FIELD.default_value = {}
LOGIN_INFOS_FIELD.message_type = EXTRAINFO
LOGIN_INFOS_FIELD.type = 11
LOGIN_INFOS_FIELD.cpp_type = 10

LOGIN.name = "Login"
LOGIN.full_name = ".LiteServer.Login"
LOGIN.nested_types = {}
LOGIN.enum_types = {}
LOGIN.fields = {LOGIN_NAME_FIELD, LOGIN_PASSWORD_FIELD, LOGIN_INFOS_FIELD}
LOGIN.is_extendable = false
LOGIN.extensions = {}
TESTMSG1_TEXT_FIELD.name = "text"
TESTMSG1_TEXT_FIELD.full_name = ".LiteServer.TestMsg1.text"
TESTMSG1_TEXT_FIELD.number = 1
TESTMSG1_TEXT_FIELD.index = 0
TESTMSG1_TEXT_FIELD.label = 1
TESTMSG1_TEXT_FIELD.has_default_value = false
TESTMSG1_TEXT_FIELD.default_value = ""
TESTMSG1_TEXT_FIELD.type = 9
TESTMSG1_TEXT_FIELD.cpp_type = 9

TESTMSG1_NUMBER_FIELD.name = "number"
TESTMSG1_NUMBER_FIELD.full_name = ".LiteServer.TestMsg1.number"
TESTMSG1_NUMBER_FIELD.number = 2
TESTMSG1_NUMBER_FIELD.index = 1
TESTMSG1_NUMBER_FIELD.label = 1
TESTMSG1_NUMBER_FIELD.has_default_value = false
TESTMSG1_NUMBER_FIELD.default_value = 0
TESTMSG1_NUMBER_FIELD.type = 5
TESTMSG1_NUMBER_FIELD.cpp_type = 1

TESTMSG1_BIGNUMBER_FIELD.name = "bignumber"
TESTMSG1_BIGNUMBER_FIELD.full_name = ".LiteServer.TestMsg1.bignumber"
TESTMSG1_BIGNUMBER_FIELD.number = 3
TESTMSG1_BIGNUMBER_FIELD.index = 2
TESTMSG1_BIGNUMBER_FIELD.label = 1
TESTMSG1_BIGNUMBER_FIELD.has_default_value = false
TESTMSG1_BIGNUMBER_FIELD.default_value = 0
TESTMSG1_BIGNUMBER_FIELD.type = 3
TESTMSG1_BIGNUMBER_FIELD.cpp_type = 2

TESTMSG1.name = "TestMsg1"
TESTMSG1.full_name = ".LiteServer.TestMsg1"
TESTMSG1.nested_types = {}
TESTMSG1.enum_types = {}
TESTMSG1.fields = {TESTMSG1_TEXT_FIELD, TESTMSG1_NUMBER_FIELD, TESTMSG1_BIGNUMBER_FIELD}
TESTMSG1.is_extendable = false
TESTMSG1.extensions = {}
TESTMSG2_TEXT_FIELD.name = "text"
TESTMSG2_TEXT_FIELD.full_name = ".LiteServer.TestMsg2.text"
TESTMSG2_TEXT_FIELD.number = 1
TESTMSG2_TEXT_FIELD.index = 0
TESTMSG2_TEXT_FIELD.label = 1
TESTMSG2_TEXT_FIELD.has_default_value = false
TESTMSG2_TEXT_FIELD.default_value = ""
TESTMSG2_TEXT_FIELD.type = 9
TESTMSG2_TEXT_FIELD.cpp_type = 9

TESTMSG2_NUMBER_FIELD.name = "number"
TESTMSG2_NUMBER_FIELD.full_name = ".LiteServer.TestMsg2.number"
TESTMSG2_NUMBER_FIELD.number = 2
TESTMSG2_NUMBER_FIELD.index = 1
TESTMSG2_NUMBER_FIELD.label = 1
TESTMSG2_NUMBER_FIELD.has_default_value = false
TESTMSG2_NUMBER_FIELD.default_value = 0
TESTMSG2_NUMBER_FIELD.type = 5
TESTMSG2_NUMBER_FIELD.cpp_type = 1

TESTMSG2_BIGNUMBER_FIELD.name = "bignumber"
TESTMSG2_BIGNUMBER_FIELD.full_name = ".LiteServer.TestMsg2.bignumber"
TESTMSG2_BIGNUMBER_FIELD.number = 3
TESTMSG2_BIGNUMBER_FIELD.index = 2
TESTMSG2_BIGNUMBER_FIELD.label = 1
TESTMSG2_BIGNUMBER_FIELD.has_default_value = false
TESTMSG2_BIGNUMBER_FIELD.default_value = 0
TESTMSG2_BIGNUMBER_FIELD.type = 3
TESTMSG2_BIGNUMBER_FIELD.cpp_type = 2

TESTMSG2.name = "TestMsg2"
TESTMSG2.full_name = ".LiteServer.TestMsg2"
TESTMSG2.nested_types = {}
TESTMSG2.enum_types = {}
TESTMSG2.fields = {TESTMSG2_TEXT_FIELD, TESTMSG2_NUMBER_FIELD, TESTMSG2_BIGNUMBER_FIELD}
TESTMSG2.is_extendable = false
TESTMSG2.extensions = {}
TESTMSG3_TEXT_FIELD.name = "text"
TESTMSG3_TEXT_FIELD.full_name = ".LiteServer.TestMsg3.text"
TESTMSG3_TEXT_FIELD.number = 1
TESTMSG3_TEXT_FIELD.index = 0
TESTMSG3_TEXT_FIELD.label = 1
TESTMSG3_TEXT_FIELD.has_default_value = false
TESTMSG3_TEXT_FIELD.default_value = ""
TESTMSG3_TEXT_FIELD.type = 9
TESTMSG3_TEXT_FIELD.cpp_type = 9

TESTMSG3_NUMBER_FIELD.name = "number"
TESTMSG3_NUMBER_FIELD.full_name = ".LiteServer.TestMsg3.number"
TESTMSG3_NUMBER_FIELD.number = 2
TESTMSG3_NUMBER_FIELD.index = 1
TESTMSG3_NUMBER_FIELD.label = 1
TESTMSG3_NUMBER_FIELD.has_default_value = false
TESTMSG3_NUMBER_FIELD.default_value = 0
TESTMSG3_NUMBER_FIELD.type = 5
TESTMSG3_NUMBER_FIELD.cpp_type = 1

TESTMSG3_BIGNUMBER_FIELD.name = "bignumber"
TESTMSG3_BIGNUMBER_FIELD.full_name = ".LiteServer.TestMsg3.bignumber"
TESTMSG3_BIGNUMBER_FIELD.number = 3
TESTMSG3_BIGNUMBER_FIELD.index = 2
TESTMSG3_BIGNUMBER_FIELD.label = 1
TESTMSG3_BIGNUMBER_FIELD.has_default_value = false
TESTMSG3_BIGNUMBER_FIELD.default_value = 0
TESTMSG3_BIGNUMBER_FIELD.type = 3
TESTMSG3_BIGNUMBER_FIELD.cpp_type = 2

TESTMSG3.name = "TestMsg3"
TESTMSG3.full_name = ".LiteServer.TestMsg3"
TESTMSG3.nested_types = {}
TESTMSG3.enum_types = {}
TESTMSG3.fields = {TESTMSG3_TEXT_FIELD, TESTMSG3_NUMBER_FIELD, TESTMSG3_BIGNUMBER_FIELD}
TESTMSG3.is_extendable = false
TESTMSG3.extensions = {}

ExtraInfo = protobuf.Message(EXTRAINFO)
Login = protobuf.Message(LOGIN)
TestMsg1 = protobuf.Message(TESTMSG1)
TestMsg2 = protobuf.Message(TESTMSG2)
TestMsg3 = protobuf.Message(TESTMSG3)

