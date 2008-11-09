
EXTRA_DIST = libs/MonoTorrent.dll 

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/monotorrent-dbus.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

MONOTORRENT_DBUS_DLL_MDB_SOURCE=bin/Debug/monotorrent-dbus.dll.mdb
MONOTORRENT_DBUS_DLL_MDB=$(BUILD_DIR)/monotorrent-dbus.dll.mdb
MONOTORRENT_DLL_SOURCE=libs/MonoTorrent.dll

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/monotorrent-dbus.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

MONOTORRENT_DBUS_DLL_MDB=
MONOTORRENT_DLL_SOURCE=libs/MonoTorrent.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONOTORRENT_DBUS_DLL_MDB) \
	$(MONOTORRENT_DLL)  

LINUX_PKGCONFIG = \
	$(MONOTORRENT_DBUS_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  

libs/MonoTorrent.dll:
	mkdir -p libs
	cp `pkg-config --variable=Libraries monotorrent` libs/MonoTorrent.dll

FILES = \
	AssemblyInfo.cs \
	Interfaces/IEngine.cs \
	Interfaces/ITracker.cs \
	Interfaces/IDownloader.cs \
	Implementation/EngineAdapter.cs \
	Implementation/TorrentManagerAdapter.cs \
	Implementation/TrackerAdapter.cs \
	Interfaces/IEngineSettings.cs \
	Implementation/EngineSettingsAdapter.cs \
	Interfaces/ITorrentSettings.cs \
	Implementation/TorrentSettingsAdapter.cs \
	Interfaces/ITorrentFile.cs \
	Implementation/TorrentFileAdapter.cs \
	Interfaces/ITorrent.cs \
	Implementation/TorrentAdapter.cs \
	Interfaces/ITorrentService.cs \
	Implementation/TorrentService.cs \
	Interfaces/IExportable.cs \
	ObjectPathComparer.cs \
	TorrentData.cs \
	Interfaces/IPeer.cs \
	Implementation/PeerAdapter.cs \
	EnumAdapters/TorrentState.cs \
	EnumAdapters/Priority.cs \
	EnumAdapters/EncryptionTypes.cs \
	EnumAdapters/EnumAdapter.cs \
	MainClass.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	DBus-API-Layout.txt \
	monotorrent-dbus.pc.in 

REFERENCES =  \
	System \
	$(NDESK_DBUS_10_LIBS) \
	System.Xml

DLL_REFERENCES =  \
	libs/MonoTorrent.dll

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

MONOTORRENT_DBUS_PC = $(BUILD_DIR)/monotorrent-dbus.pc
MONOTORRENT_DLL = $(BUILD_DIR)/MonoTorrent.dll

$(eval $(call emit-deploy-wrapper,MONOTORRENT_DBUS_PC,monotorrent-dbus.pc))
$(eval $(call emit-deploy-target,MONOTORRENT_DLL))

dbusservice_in_files = \
	data/org.monotorrent.dbus.service.in
 
dbusservicedir = $(datadir)/dbus-1/services
dbusservice_DATA = $(dbusservice_in_files:.service.in=.service)

$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
