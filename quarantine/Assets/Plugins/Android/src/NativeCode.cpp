//#include "string_message.pb.h"
#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <netinet/in.h>

#define BUFFSIZE 10000 // inefficient. Please fix me with dynamic memory allocation

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

	int createTCPSocket(const char* hostname, const char* port, char* error)
	{
		struct addrinfo hints, *res;
		int sockfd, status;

		memset(&hints, 0, sizeof hints);
		hints.ai_family = AF_INET;
		hints.ai_socktype = SOCK_STREAM;

		if((status = getaddrinfo(hostname, port, &hints, &res)) != 0) {
			sprintf(error, "getaddrinfo: %s\n", gai_strerror(status));
			return -1;
		}

		sockfd = socket(res->ai_family, res->ai_socktype, res->ai_protocol);

		if((status = connect(sockfd, res->ai_addr, res->ai_addrlen)) != 0)
		{
			close(sockfd);
			sprintf(error, "connect: %s\n", gai_strerror(status));
			return -1;
		}

		return sockfd;
	}

	int sendMsg(int sockfd, const char* msg, int len)
	{
		int bytes_sent;
		bytes_sent = 0;

		while((bytes_sent += send(sockfd, msg, len, 0)) < len) {}
		
		return bytes_sent;
	}

	int recvMsg(int sockfd, char* msg, int len)
	{
		char buf[BUFFSIZE];
		int bytes_recvd;

		// this is wrong, but a precursor to right
		bytes_recvd = recv(sockfd, msg, len-1, 0);
		msg[bytes_recvd] = '\0';

		return bytes_recvd;
	}

	void closeSocket(int sockfd)
	{
		close(sockfd);
	}
}
