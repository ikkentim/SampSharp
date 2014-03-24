SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_HTTP(int index, int type, const char * url, const char * data);

#ifndef __cplusplus

#define HTTP_GET (1)
#define HTTP_POST (2)
#define HTTP_HEAD (3)
#define HTTP_ERROR_BAD_HOST (1)
#define HTTP_ERROR_NO_SOCKET (2)
#define HTTP_ERROR_CANT_CONNECT (3)
#define HTTP_ERROR_CANT_WRITE (4)
#define HTTP_ERROR_CONTENT_TOO_BIG (5)
#define HTTP_ERROR_MALFORMED_RESPONSE (6)

#undef  HTTP
#define HTTP sampgdk_HTTP

#else /* __cplusplus */

SAMPGDK_BEGIN_NAMESPACE

const int HTTP_GET = 1;
const int HTTP_POST = 2;
const int HTTP_HEAD = 3;
const int HTTP_ERROR_BAD_HOST = 1;
const int HTTP_ERROR_NO_SOCKET = 2;
const int HTTP_ERROR_CANT_CONNECT = 3;
const int HTTP_ERROR_CANT_WRITE = 4;
const int HTTP_ERROR_CONTENT_TOO_BIG = 5;
const int HTTP_ERROR_MALFORMED_RESPONSE = 6;

static inline bool HTTP(int index, int type, const char * url, const char * data) {
  return ::sampgdk_HTTP(index, type, url, data, callback);
}

SAMPGDK_END_NAMESPACE

#endif /* __cplusplus */

SAMPGDK_CALLBACK_EXPORT void SAMPGDK_CALLBACK_CALL OnHTTPResponse(int index, int response_code, const char * data);
