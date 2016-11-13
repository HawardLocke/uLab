-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
module('login_pb')


local LOGINROLEINFO = protobuf.Descriptor();
local LOGINROLEINFO_INDEX_FIELD = protobuf.FieldDescriptor();
local LOGINROLEINFO_ID_FIELD = protobuf.FieldDescriptor();
local LOGINROLEINFO_NAME_FIELD = protobuf.FieldDescriptor();
local LOGINROLEINFO_CAREER_FIELD = protobuf.FieldDescriptor();
local LOGINROLEINFO_LEVEL_FIELD = protobuf.FieldDescriptor();
local LOGINREQUEST = protobuf.Descriptor();
local LOGINREQUEST_ACCOUNT_FIELD = protobuf.FieldDescriptor();
local LOGINREQUEST_PASSWORD_FIELD = protobuf.FieldDescriptor();
local LOGINRESPONSE = protobuf.Descriptor();
local LOGINRESPONSE_RESULT_FIELD = protobuf.FieldDescriptor();
local ENTERGAMEREQUEST = protobuf.Descriptor();
local ENTERGAMEREQUEST_ROLEINDEX_FIELD = protobuf.FieldDescriptor();
local ENTERGAMERESPONSE = protobuf.Descriptor();
local ENTERGAMERESPONSE_RESULT_FIELD = protobuf.FieldDescriptor();

LOGINROLEINFO_INDEX_FIELD.name = "index"
LOGINROLEINFO_INDEX_FIELD.full_name = ".Lite.LoginRoleInfo.index"
LOGINROLEINFO_INDEX_FIELD.number = 1
LOGINROLEINFO_INDEX_FIELD.index = 0
LOGINROLEINFO_INDEX_FIELD.label = 1
LOGINROLEINFO_INDEX_FIELD.has_default_value = false
LOGINROLEINFO_INDEX_FIELD.default_value = 0
LOGINROLEINFO_INDEX_FIELD.type = 5
LOGINROLEINFO_INDEX_FIELD.cpp_type = 1

LOGINROLEINFO_ID_FIELD.name = "id"
LOGINROLEINFO_ID_FIELD.full_name = ".Lite.LoginRoleInfo.id"
LOGINROLEINFO_ID_FIELD.number = 2
LOGINROLEINFO_ID_FIELD.index = 1
LOGINROLEINFO_ID_FIELD.label = 1
LOGINROLEINFO_ID_FIELD.has_default_value = false
LOGINROLEINFO_ID_FIELD.default_value = 0
LOGINROLEINFO_ID_FIELD.type = 3
LOGINROLEINFO_ID_FIELD.cpp_type = 2

LOGINROLEINFO_NAME_FIELD.name = "name"
LOGINROLEINFO_NAME_FIELD.full_name = ".Lite.LoginRoleInfo.name"
LOGINROLEINFO_NAME_FIELD.number = 3
LOGINROLEINFO_NAME_FIELD.index = 2
LOGINROLEINFO_NAME_FIELD.label = 1
LOGINROLEINFO_NAME_FIELD.has_default_value = false
LOGINROLEINFO_NAME_FIELD.default_value = ""
LOGINROLEINFO_NAME_FIELD.type = 9
LOGINROLEINFO_NAME_FIELD.cpp_type = 9

LOGINROLEINFO_CAREER_FIELD.name = "career"
LOGINROLEINFO_CAREER_FIELD.full_name = ".Lite.LoginRoleInfo.career"
LOGINROLEINFO_CAREER_FIELD.number = 4
LOGINROLEINFO_CAREER_FIELD.index = 3
LOGINROLEINFO_CAREER_FIELD.label = 1
LOGINROLEINFO_CAREER_FIELD.has_default_value = false
LOGINROLEINFO_CAREER_FIELD.default_value = 0
LOGINROLEINFO_CAREER_FIELD.type = 5
LOGINROLEINFO_CAREER_FIELD.cpp_type = 1

LOGINROLEINFO_LEVEL_FIELD.name = "level"
LOGINROLEINFO_LEVEL_FIELD.full_name = ".Lite.LoginRoleInfo.level"
LOGINROLEINFO_LEVEL_FIELD.number = 5
LOGINROLEINFO_LEVEL_FIELD.index = 4
LOGINROLEINFO_LEVEL_FIELD.label = 1
LOGINROLEINFO_LEVEL_FIELD.has_default_value = false
LOGINROLEINFO_LEVEL_FIELD.default_value = 0
LOGINROLEINFO_LEVEL_FIELD.type = 5
LOGINROLEINFO_LEVEL_FIELD.cpp_type = 1

LOGINROLEINFO.name = "LoginRoleInfo"
LOGINROLEINFO.full_name = ".Lite.LoginRoleInfo"
LOGINROLEINFO.nested_types = {}
LOGINROLEINFO.enum_types = {}
LOGINROLEINFO.fields = {LOGINROLEINFO_INDEX_FIELD, LOGINROLEINFO_ID_FIELD, LOGINROLEINFO_NAME_FIELD, LOGINROLEINFO_CAREER_FIELD, LOGINROLEINFO_LEVEL_FIELD}
LOGINROLEINFO.is_extendable = false
LOGINROLEINFO.extensions = {}
LOGINREQUEST_ACCOUNT_FIELD.name = "account"
LOGINREQUEST_ACCOUNT_FIELD.full_name = ".Lite.LoginRequest.account"
LOGINREQUEST_ACCOUNT_FIELD.number = 1
LOGINREQUEST_ACCOUNT_FIELD.index = 0
LOGINREQUEST_ACCOUNT_FIELD.label = 1
LOGINREQUEST_ACCOUNT_FIELD.has_default_value = false
LOGINREQUEST_ACCOUNT_FIELD.default_value = ""
LOGINREQUEST_ACCOUNT_FIELD.type = 9
LOGINREQUEST_ACCOUNT_FIELD.cpp_type = 9

LOGINREQUEST_PASSWORD_FIELD.name = "password"
LOGINREQUEST_PASSWORD_FIELD.full_name = ".Lite.LoginRequest.password"
LOGINREQUEST_PASSWORD_FIELD.number = 2
LOGINREQUEST_PASSWORD_FIELD.index = 1
LOGINREQUEST_PASSWORD_FIELD.label = 1
LOGINREQUEST_PASSWORD_FIELD.has_default_value = false
LOGINREQUEST_PASSWORD_FIELD.default_value = ""
LOGINREQUEST_PASSWORD_FIELD.type = 9
LOGINREQUEST_PASSWORD_FIELD.cpp_type = 9

LOGINREQUEST.name = "LoginRequest"
LOGINREQUEST.full_name = ".Lite.LoginRequest"
LOGINREQUEST.nested_types = {}
LOGINREQUEST.enum_types = {}
LOGINREQUEST.fields = {LOGINREQUEST_ACCOUNT_FIELD, LOGINREQUEST_PASSWORD_FIELD}
LOGINREQUEST.is_extendable = false
LOGINREQUEST.extensions = {}
LOGINRESPONSE_RESULT_FIELD.name = "result"
LOGINRESPONSE_RESULT_FIELD.full_name = ".Lite.LoginResponse.result"
LOGINRESPONSE_RESULT_FIELD.number = 1
LOGINRESPONSE_RESULT_FIELD.index = 0
LOGINRESPONSE_RESULT_FIELD.label = 1
LOGINRESPONSE_RESULT_FIELD.has_default_value = false
LOGINRESPONSE_RESULT_FIELD.default_value = 0
LOGINRESPONSE_RESULT_FIELD.type = 5
LOGINRESPONSE_RESULT_FIELD.cpp_type = 1

LOGINRESPONSE.name = "LoginResponse"
LOGINRESPONSE.full_name = ".Lite.LoginResponse"
LOGINRESPONSE.nested_types = {}
LOGINRESPONSE.enum_types = {}
LOGINRESPONSE.fields = {LOGINRESPONSE_RESULT_FIELD}
LOGINRESPONSE.is_extendable = false
LOGINRESPONSE.extensions = {}
ENTERGAMEREQUEST_ROLEINDEX_FIELD.name = "roleIndex"
ENTERGAMEREQUEST_ROLEINDEX_FIELD.full_name = ".Lite.EnterGameRequest.roleIndex"
ENTERGAMEREQUEST_ROLEINDEX_FIELD.number = 1
ENTERGAMEREQUEST_ROLEINDEX_FIELD.index = 0
ENTERGAMEREQUEST_ROLEINDEX_FIELD.label = 1
ENTERGAMEREQUEST_ROLEINDEX_FIELD.has_default_value = false
ENTERGAMEREQUEST_ROLEINDEX_FIELD.default_value = 0
ENTERGAMEREQUEST_ROLEINDEX_FIELD.type = 5
ENTERGAMEREQUEST_ROLEINDEX_FIELD.cpp_type = 1

ENTERGAMEREQUEST.name = "EnterGameRequest"
ENTERGAMEREQUEST.full_name = ".Lite.EnterGameRequest"
ENTERGAMEREQUEST.nested_types = {}
ENTERGAMEREQUEST.enum_types = {}
ENTERGAMEREQUEST.fields = {ENTERGAMEREQUEST_ROLEINDEX_FIELD}
ENTERGAMEREQUEST.is_extendable = false
ENTERGAMEREQUEST.extensions = {}
ENTERGAMERESPONSE_RESULT_FIELD.name = "result"
ENTERGAMERESPONSE_RESULT_FIELD.full_name = ".Lite.EnterGameResponse.result"
ENTERGAMERESPONSE_RESULT_FIELD.number = 1
ENTERGAMERESPONSE_RESULT_FIELD.index = 0
ENTERGAMERESPONSE_RESULT_FIELD.label = 1
ENTERGAMERESPONSE_RESULT_FIELD.has_default_value = false
ENTERGAMERESPONSE_RESULT_FIELD.default_value = 0
ENTERGAMERESPONSE_RESULT_FIELD.type = 5
ENTERGAMERESPONSE_RESULT_FIELD.cpp_type = 1

ENTERGAMERESPONSE.name = "EnterGameResponse"
ENTERGAMERESPONSE.full_name = ".Lite.EnterGameResponse"
ENTERGAMERESPONSE.nested_types = {}
ENTERGAMERESPONSE.enum_types = {}
ENTERGAMERESPONSE.fields = {ENTERGAMERESPONSE_RESULT_FIELD}
ENTERGAMERESPONSE.is_extendable = false
ENTERGAMERESPONSE.extensions = {}

EnterGameRequest = protobuf.Message(ENTERGAMEREQUEST)
EnterGameResponse = protobuf.Message(ENTERGAMERESPONSE)
LoginRequest = protobuf.Message(LOGINREQUEST)
LoginResponse = protobuf.Message(LOGINRESPONSE)
LoginRoleInfo = protobuf.Message(LOGINROLEINFO)

