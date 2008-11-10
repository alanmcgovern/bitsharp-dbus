
EXTRA_DIST =  

ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/monotorrent-dbus-server.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../monotorrent-dbus/bin/Release/monotorrent-dbus.dll
BUILD_DIR = bin/Release

MONOTORRENT_DLL_SOURCE=./libs/MonoTorrent.dll
MONOTORRENT_DBUS_DLL_SOURCE=../monotorrent-dbus/bin/Release/monotorrent-dbus.dll
MONOTORRENT_DBUS_SERVER_EXE_MDB=

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONOTORRENT_DLL) \
	$(MONOTORRENT_DBUS_DLL) \
	$(MONOTORRENT_DBUS_SERVER_EXE_MDB)  

BINARIES = \
	$(MONOTORRENT_DBUS_SERVER)  


RESGEN=resgen2
	
all: ./libs/MonoTorrent.dll $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

./libs/MonoTorrent.dll:
	mkdir -p libs
	cp `pkg-config --variable=Libraries monotorrent` libs/MonoTorrent.dll

FILES = \
	Main.cs \
	AssemblyInfo.cs \
	Implementation/EngineAdapter.cs \
	Implementation/EngineSettingsAdapter.cs \
	Implementation/PeerAdapter.cs \
	Implementation/TorrentAdapter.cs \
	Implementation/TorrentFileAdapter.cs \
	Implementation/TorrentManagerAdapter.cs \
	Implementation/TorrentService.cs \
	Implementation/TorrentSettingsAdapter.cs \
	Implementation/TrackerAdapter.cs \
	Implementation/EnumAdapter.cs \
	TorrentData.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	monotorrent-dbus-server.in 

REFERENCES =  \
	System \
	$(NDESK_DBUS_10_LIBS) \
	System.Xml

DLL_REFERENCES =  \
	$(MONOTORRENT_DLL)

CLEANFILES = $(PROGRAMFILES) $(BINARIES) $(DLL_REFERENCES) $(MONOTORRENT_DLL_SOURCE)

include $(top_srcdir)/Makefile.include

MONOTORRENT_DBUS_SERVER = $(BUILD_DIR)/monotorrent-dbus-server
MONOTORRENT_DLL = $(BUILD_DIR)/MonoTorrent.dll
MONOTORRENT_DBUS_DLL = $(BUILD_DIR)/monotorrent-dbus.dll

$(eval $(call emit-deploy-wrapper,MONOTORRENT_DBUS_SERVER,monotorrent-dbus-server,x))
$(eval $(call emit-deploy-target,MONOTORRENT_DLL))
$(eval $(call emit-deploy-target,MONOTORRENT_DBUS_DLL))

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
