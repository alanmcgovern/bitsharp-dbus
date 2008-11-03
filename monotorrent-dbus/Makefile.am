
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/monotorrent-dbus.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

MONOTORRENT_DBUS_EXE_MDB_SOURCE=bin/Debug/monotorrent-dbus.exe.mdb
MONOTORRENT_DBUS_EXE_MDB=$(BUILD_DIR)/monotorrent-dbus.exe.mdb
MONOTORRENT_DLL_SOURCE=../libs/MonoTorrent.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/monotorrent-dbus.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

MONOTORRENT_DBUS_EXE_MDB=
MONOTORRENT_DLL_SOURCE=../libs/MonoTorrent.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=MonoTorrent.DBus.resources.dll

PROGRAMFILES = \
	$(MONOTORRENT_DBUS_EXE_MDB) \
	$(MONOTORRENT_DLL)  

BINARIES = \
	$(MONOTORRENT_DBUS)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES =  \
	AssemblyInfo.cs \
	EnumAdapters/EncryptionTypes.cs \
	EnumAdapters/EnumAdapter.cs \
	EnumAdapters/Priority.cs \
	EnumAdapters/TorrentState.cs \
	Implementation/EngineAdapter.cs \
	Implementation/EngineSettingsAdapter.cs \
	Implementation/PeerAdapter.cs \
	Implementation/TorrentAdapter.cs \
	Implementation/TorrentFileAdapter.cs \
	Implementation/TorrentManagerAdapter.cs \
	Implementation/TorrentService.cs \
	Implementation/TorrentSettingsAdapter.cs \
	Implementation/TrackerAdapter.cs \
	Interfaces/IDownloader.cs \
	Interfaces/IEngine.cs \
	Interfaces/IEngineSettings.cs \
	Interfaces/IExportable.cs \
	Interfaces/IPeer.cs \
	Interfaces/ITorrent.cs \
	Interfaces/ITorrentFile.cs \
	Interfaces/ITorrentService.cs \
	Interfaces/ITorrentSettings.cs \
	Interfaces/ITracker.cs \
	MainClass.cs \
	ObjectPathComparer.cs \
	TorrentData.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	DBus-API-Layout.txt \
	monotorrent-dbus.in 

REFERENCES =  \
	System \
	$(NDESK_DBUS_10_LIBS) \
	System.Xml

DLL_REFERENCES =  \
	../libs/MonoTorrent.dll

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

MONOTORRENT_DBUS = $(BUILD_DIR)/monotorrent-dbus
MONOTORRENT_DLL = $(BUILD_DIR)/MonoTorrent.dll

$(eval $(call emit-deploy-wrapper,MONOTORRENT_DBUS,monotorrent-dbus,x))
$(eval $(call emit-deploy-target,MONOTORRENT_DLL))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
