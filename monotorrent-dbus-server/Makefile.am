
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/monotorrent-dbus-server.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../monotorrent-dbus/bin/Debug/monotorrent-dbus.dll
BUILD_DIR = bin/Debug

MONOTORRENT_DBUS_DLL_SOURCE=../monotorrent-dbus/bin/Debug/monotorrent-dbus.dll
MONOTORRENT_DBUS_SERVER_EXE_MDB_SOURCE=bin/Debug/monotorrent-dbus-server.exe.mdb
MONOTORRENT_DBUS_SERVER_EXE_MDB=$(BUILD_DIR)/monotorrent-dbus-server.exe.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/monotorrent-dbus-server.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../monotorrent-dbus/bin/Release/monotorrent-dbus.dll
BUILD_DIR = bin/Release

MONOTORRENT_DBUS_DLL_SOURCE=../monotorrent-dbus/bin/Release/monotorrent-dbus.dll
MONOTORRENT_DBUS_SERVER_EXE_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONOTORRENT_DBUS_DLL) \
	$(MONOTORRENT_DBUS_SERVER_EXE_MDB)  

BINARIES = \
	$(MONOTORRENT_DBUS_SERVER)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES =  \
	AssemblyInfo.cs \
	Implementation/EngineAdapter.cs \
	Implementation/EngineSettingsAdapter.cs \
	Implementation/EnumAdapter.cs \
	Implementation/PeerAdapter.cs \
	Implementation/TorrentAdapter.cs \
	Implementation/TorrentFileAdapter.cs \
	Implementation/TorrentManagerAdapter.cs \
	Implementation/TorrentService.cs \
	Implementation/TorrentSettingsAdapter.cs \
	Implementation/TrackerAdapter.cs \
	Main.cs \
	MainClass.cs \
	TorrentData.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	monotorrent-dbus-server.in 

REFERENCES =  \
	../monotorrent-dbus/bin/Debug/monotorrent-dbus.dll \
	-pkg:ndesk-dbus-1.0 \
	System

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

MONOTORRENT_DBUS_SERVER = $(BUILD_DIR)/monotorrent-dbus-server
MONOTORRENT_DBUS_DLL = $(BUILD_DIR)/monotorrent-dbus.dll

$(eval $(call emit-deploy-wrapper,MONOTORRENT_DBUS_SERVER,monotorrent-dbus-server,x))
$(eval $(call emit-deploy-target,MONOTORRENT_DBUS_DLL))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
