
EXTRA_DIST =  expansions.m4 libs/MonoTorrent.dll

#Warning: This is an automatically generated file, do not edit!
if ENABLE_DEBUG
 SUBDIRS =  monotorrent-dbus
endif
if ENABLE_RELEASE
 SUBDIRS =  monotorrent-dbus
endif

update-libraries:
	mkdir -p libs
	cp `pkg-config --variable=Libraries monotorrent` ./libs/MonoTorrent.dll
#pkg-config --libs monotorrent > monotorrent.flags

run:
	mono monotorrent-dbus/bin/Release/monotorrent-dbus.exe
