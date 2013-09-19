#LOCAL_PATH := $(call my-dir)
#include $(LOCAL_PATH)/protobuf/Android.mk




#include $(CLEAR_VARS)

# protobuf includes
#LOCAL_MODULE := protobuf
#LOCAL_MODULE_FILENAME := libprotobuf
#LOCAL_SRC_FILES := libprotobuf.so

#include $(PREBUILD_SHARED_LIBRARY)

include $(CLEAR_VARS)

LOCAL_PATH := $(NDK_PROJECT_PATH)
LOCAL_MODULE := libnative
LOCAL_CPP_EXTENSION := .cpp .cc
LOCAL_SRC_FILES := NativeCode.cpp # string_message.pb.cc
#LOCAL_C_INCLUDES := $(LOCAL_PATH)/. /usr/local/include/


#LOCAL_LDLIBS := -lprotobuf

#LOCAL_CPP_EXTENSION := .cpp .cc
#LOCAL_ARM_MODE  := arm
#LOCAL_PATH      := $(NDK_PROJECT_PATH)
#LOCAL_MODULE    := libnative
#LOCAL_CFLAGS    := -Werror
#LOCAL_SRC_FILES := NativeCode.cpp string_message.pb.cc
#LOCAL_LDLIBS    := -llog
#LOCAL_SHARED_LIBRARIES := protobuf

include $(BUILD_SHARED_LIBRARY)
