-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
module('login_pb')


local GCLOGINROLEINFO = protobuf.Descriptor();
local GCLOGINROLEINFO_INDEX_FIELD = protobuf.FieldDescriptor();
local GCLOGINROLEINFO_ID_FIELD = protobuf.FieldDescriptor();
local GCLOGINROLEINFO_NAME_FIELD = protobuf.FieldDescriptor();
local GCLOGINROLEINFO_CAREER_FIELD = protobuf.FieldDescriptor();
local GCLOGINROLEINFO_LEVEL_FIELD = protobuf.FieldDescriptor();
local CGLOGIN = protobuf.Descriptor();
local CGLOGIN_ACCOUNT_FIELD = protobuf.FieldDescriptor();
local CGLOGIN_PASSWORD_FIELD = protobuf.FieldDescriptor();
local GCLOGINRET = protobuf.Descriptor();
local GCLOGINRET_RESULT_FIELD = protobuf.FieldDescriptor();
local CGLOGOUT = protobuf.Descriptor();
local GCLOGOUTRET = protobuf.Descriptor();
local GCLOGOUTRET_RESULT_FIELD = protobuf.FieldDescriptor();
local CGENTERGAME = protobuf.Descriptor();
local CGENTERGAME_ROLEINDEX_FIELD = protobuf.FieldDescriptor();
local GCENTERGAMERET = protobuf.Descriptor();
local GCENTERGAMERET_RESULT_FIELD = protobuf.FieldDescriptor();

GCLOGINROLEINFO_INDEX_FIELD.name = "index"
GCLOGINROLEINFO_INDEX_FIELD.full_name = ".Lite.Protocol.gcLoginRoleInfo.index"
GCLOGINROLEINFO_INDEX_FIELD.number = 1
GCLOGINROLEINFO_INDEX_FIELD.index = 0
GCLOGINROLEINFO_INDEX_FIELD.label = 1
GCLOGINROLEINFO_INDEX_FIELD.has_default_value = false
GCLOGINROLEINFO_INDEX_FIELD.default_value = 0
GCLOGINROLEINFO_INDEX_FIELD.type = 5
GCLOGINROLEINFO_INDEX_FIELD.cpp_type = 1

GCLOGINROLEINFO_ID_FIELD.name = "id"
GCLOGINROLEINFO_ID_FIELD.full_name = ".Lite.Protocol.gcLoginRoleInfo.id"
GCLOGINROLEINFO_ID_FIELD.number = 2
GCLOGINROLEINFO_ID_FIELD.index = 1
GCLOGINROLEINFO_ID_FIELD.label = 1
GCLOGINROLEINFO_ID_FIELD.has_default_value = false
GCLOGINROLEINFO_ID_FIELD.default_value = 0
GCLOGINROLEINFO_ID_FIELD.type = 3
GCLOGINROLEINFO_ID_FIELD.cpp_type = 2

GCLOGINROLEINFO_NAME_FIELD.name = "name"
GCLOGINROLEINFO_NAME_FIELD.full_name = ".Lite.Protocol.gcLoginRoleInfo.name"
GCLOGINROLEINFO_NAME_FIELD.number = 3
GCLOGINROLEINFO_NAME_FIELD.index = 2
GCLOGINROLEINFO_NAME_FIELD.label = 1
GCLOGINROLEINFO_NAME_FIELD.has_default_value = false
GCLOGINROLEINFO_NAME_FIELD.default_value = ""
GCLOGINROLEINFO_NAME_FIELD.type = 9
GCLOGINROLEINFO_NAME_FIELD.cpp_type = 9

GCLOGINROLEINFO_CAREER_FIELD.name = "career"
GCLOGINROLEINFO_CAREER_FIELD.full_name = ".Lite.Protocol.gcLoginRoleInfo.career"
GCLOGINROLEINFO_CAREER_FIELD.number = 4
GCLOGINROLEINFO_CAREER_FIELD.index = 3
GCLOGINROLEINFO_CAREER_FIELD.label = 1
GCLOGINROLEINFO_CAREER_FIELD.has_default_value = false
GCLOGINROLEINFO_CAREER_FIELD.default_value = 0
GCLOGINROLEINFO_CAREER_FIELD.type = 5
GCLOGINROLEINFO_CAREER_FIELD.cpp_type = 1

GCLOGINROLEINFO_LEVEL_FIELD.name = "level"
GCLOGINROLEINFO_LEVEL_FIELD.full_name = ".Lite.Protocol.gcLoginRoleInfo.level"
GCLOGINROLEINFO_LEVEL_FIELD.number = 5
GCLOGINROLEINFO_LEVEL_FIELD.index = 4
GCLOGINROLEINFO_LEVEL_FIELD.label = 1
GCLOGINROLEINFO_LEVEL_FIELD.has_default_value = false
GCLOGINROLEINFO_LEVEL_FIELD.default_value = 0
GCLOGINROLEINFO_LEVEL_FIELD.type = 5
GCLOGINROLEINFO_LEVEL_FIELD.cpp_type = 1

GCLOGINROLEINFO.name = "gcLoginRoleInfo"
GCLOGINROLEINFO.full_name = ".Lite.Protocol.gcLoginRoleInfo"
GCLOGINROLEINFO.nested_types = {}
GCLOGINROLEINFO.enum_types = {}
GCLOGINROLEINFO.fields = {GCLOGINROLEINFO_INDEX_FIELD, GCLOGINROLEINFO_ID_FIELD, GCLOGINROLEINFO_NAME_FIELD, GCLOGINROLEINFO_CAREER_FIELD, GCLOGINROLEINFO_LEVEL_FIELD}
GCLOGINROLEINFO.is_extendable = false
GCLOGINROLEINFO.extensions = {}
CGLOGIN_ACCOUNT_FIELD.name = "account"
CGLOGIN_ACCOUNT_FIELD.full_name = ".Lite.Protocol.cgLogin.account"
CGLOGIN_ACCOUNT_FIELD.number = 1
CGLOGIN_ACCOUNT_FIELD.index = 0
CGLOGIN_ACCOUNT_FIELD.label = 1
CGLOGIN_ACCOUNT_FIELD.has_default_value = false
CGLOGIN_ACCOUNT_FIELD.default_value = ""
CGLOGIN_ACCOUNT_FIELD.type = 9
CGLOGIN_ACCOUNT_FIELD.cpp_type = 9

CGLOGIN_PASSWORD_FIELD.name = "password"
CGLOGIN_PASSWORD_FIELD.full_name = ".Lite.Protocol.cgLogin.password"
CGLOGIN_PASSWORD_FIELD.number = 2
CGLOGIN_PASSWORD_FIELD.index = 1
CGLOGIN_PASSWORD_FIELD.label = 1
CGLOGIN_PASSWORD_FIELD.has_default_value = false
CGLOGIN_PASSWORD_FIELD.default_value = ""
CGLOGIN_PASSWORD_FIELD.type = 9
CGLOGIN_PASSWORD_FIELD.cpp_type = 9

CGLOGIN.name = "cgLogin"
CGLOGIN.full_name = ".Lite.Protocol.cgLogin"
CGLOGIN.nested_types = {}
CGLOGIN.enum_types = {}
CGLOGIN.fields = {CGLOGIN_ACCOUNT_FIELD, CGLOGIN_PASSWORD_FIELD}
CGLOGIN.is_extendable = false
CGLOGIN.extensions = {}
GCLOGINRET_RESULT_FIELD.name = "result"
GCLOGINRET_RESULT_FIELD.full_name = ".Lite.Protocol.gcLoginRet.result"
GCLOGINRET_RESULT_FIELD.number = 1
GCLOGINRET_RESULT_FIELD.index = 0
GCLOGINRET_RESULT_FIELD.label = 1
GCLOGINRET_RESULT_FIELD.has_default_value = false
GCLOGINRET_RESULT_FIELD.default_value = 0
GCLOGINRET_RESULT_FIELD.type = 5
GCLOGINRET_RESULT_FIELD.cpp_type = 1

GCLOGINRET.name = "gcLoginRet"
GCLOGINRET.full_name = ".Lite.Protocol.gcLoginRet"
GCLOGINRET.nested_types = {}
GCLOGINRET.enum_types = {}
GCLOGINRET.fields = {GCLOGINRET_RESULT_FIELD}
GCLOGINRET.is_extendable = false
GCLOGINRET.extensions = {}
CGLOGOUT.name = "cgLogout"
CGLOGOUT.full_name = ".Lite.Protocol.cgLogout"
CGLOGOUT.nested_types = {}
CGLOGOUT.enum_types = {}
CGLOGOUT.fields = {}
CGLOGOUT.is_extendable = false
CGLOGOUT.extensions = {}
GCLOGOUTRET_RESULT_FIELD.name = "result"
GCLOGOUTRET_RESULT_FIELD.full_name = ".Lite.Protocol.gcLogoutRet.result"
GCLOGOUTRET_RESULT_FIELD.number = 1
GCLOGOUTRET_RESULT_FIELD.index = 0
GCLOGOUTRET_RESULT_FIELD.label = 1
GCLOGOUTRET_RESULT_FIELD.has_default_value = false
GCLOGOUTRET_RESULT_FIELD.default_value = 0
GCLOGOUTRET_RESULT_FIELD.type = 5
GCLOGOUTRET_RESULT_FIELD.cpp_type = 1

GCLOGOUTRET.name = "gcLogoutRet"
GCLOGOUTRET.full_name = ".Lite.Protocol.gcLogoutRet"
GCLOGOUTRET.nested_types = {}
GCLOGOUTRET.enum_types = {}
GCLOGOUTRET.fields = {GCLOGOUTRET_RESULT_FIELD}
GCLOGOUTRET.is_extendable = false
GCLOGOUTRET.extensions = {}
CGENTERGAME_ROLEINDEX_FIELD.name = "roleIndex"
CGENTERGAME_ROLEINDEX_FIELD.full_name = ".Lite.Protocol.cgEnterGame.roleIndex"
CGENTERGAME_ROLEINDEX_FIELD.number = 1
CGENTERGAME_ROLEINDEX_FIELD.index = 0
CGENTERGAME_ROLEINDEX_FIELD.label = 1
CGENTERGAME_ROLEINDEX_FIELD.has_default_value = false
CGENTERGAME_ROLEINDEX_FIELD.default_value = 0
CGENTERGAME_ROLEINDEX_FIELD.type = 5
CGENTERGAME_ROLEINDEX_FIELD.cpp_type = 1

CGENTERGAME.name = "cgEnterGame"
CGENTERGAME.full_name = ".Lite.Protocol.cgEnterGame"
CGENTERGAME.nested_types = {}
CGENTERGAME.enum_types = {}
CGENTERGAME.fields = {CGENTERGAME_ROLEINDEX_FIELD}
CGENTERGAME.is_extendable = false
CGENTERGAME.extensions = {}
GCENTERGAMERET_RESULT_FIELD.name = "result"
GCENTERGAMERET_RESULT_FIELD.full_name = ".Lite.Protocol.gcEnterGameRet.result"
GCENTERGAMERET_RESULT_FIELD.number = 1
GCENTERGAMERET_RESULT_FIELD.index = 0
GCENTERGAMERET_RESULT_FIELD.label = 1
GCENTERGAMERET_RESULT_FIELD.has_default_value = false
GCENTERGAMERET_RESULT_FIELD.default_value = 0
GCENTERGAMERET_RESULT_FIELD.type = 5
GCENTERGAMERET_RESULT_FIELD.cpp_type = 1

GCENTERGAMERET.name = "gcEnterGameRet"
GCENTERGAMERET.full_name = ".Lite.Protocol.gcEnterGameRet"
GCENTERGAMERET.nested_types = {}
GCENTERGAMERET.enum_types = {}
GCENTERGAMERET.fields = {GCENTERGAMERET_RESULT_FIELD}
GCENTERGAMERET.is_extendable = false
GCENTERGAMERET.extensions = {}

cgEnterGame = protobuf.Message(CGENTERGAME)
cgLogin = protobuf.Message(CGLOGIN)
cgLogout = protobuf.Message(CGLOGOUT)
gcEnterGameRet = protobuf.Message(GCENTERGAMERET)
gcLoginRet = protobuf.Message(GCLOGINRET)
gcLoginRoleInfo = protobuf.Message(GCLOGINROLEINFO)
gcLogoutRet = protobuf.Message(GCLOGOUTRET)

