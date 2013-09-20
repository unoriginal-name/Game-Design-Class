//#include "string_message.pb.h"
#include <stdio.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <netinet/in.h>


extern "C" {
	
	int add(int x, int y)
	{
		return x + y;
	}
		
	void helloString(char* dest, const char* src, size_t n)
    {
		strncpy(dest, src, n);
    }

	void getDNS(char* hostname, char* result, size_t n)
	{
		struct addrinfo hints, *res, *p;
		int status;
		char ipstr[INET6_ADDRSTRLEN];

		memset(&hints, 0, sizeof hints);
		hints.ai_family = AF_UNSPEC; // AF_INET or AF_INET6 to force version
		hints.ai_socktype = SOCK_STREAM;

		if((status=getaddrinfo(hostname, NULL, &hints, &res)) != 0) {
			sprintf(result, "Error with hostname %s getaddrinfo: %s\n", hostname, gai_strerror(status));
			return;
		}

		sprintf(result, "IP addresses for %s:\n\n", hostname);

		for(p = res; p != NULL; p = p->ai_next) {
			void *addr;
			char *ipver;

			// get the pointer to the address itself
			// different fields in IPv4 and IPv6
			if (p->ai_family == AF_INET) { // IPv4
				struct sockaddr_in *ipv4 = (struct sockaddr_in*)p->ai_addr;
				addr = &(ipv4->sin_addr);
				ipver = "IPv4";
			} else { // IPv6
				struct sockaddr_in6 *ipv6 = (struct sockaddr_in6*)p->ai_addr;
				addr = &(ipv6->sin6_addr);
				ipver = "IPv6";
			}

			// convert the IP to a string and print it
			inet_ntop(p->ai_family, addr, ipstr, sizeof ipstr);
			if(strlen(result) < n)
				sprintf(result + strlen(result), " %s: %s\n", ipver, ipstr);
		}

		freeaddrinfo(res); // free the linked list

		return;
	}
	
}
