# TreeQueryAPI
.Net Core 3.0 VS 2022

An RESTful API that queries a tree structure and return data about specific nodes and their children

Built using Visual Studio Community Edition 2019, utilising .NET Core 3.0

Gets all nodes.
https://localhost:44321/api/node/get

Get node by identity
https://localhost:44321/api/node/get/c94342cc-0dff-483e-887a-1af3abc5612f

Get node by case insensitive name.
https://localhost:44321/api/node/getbyname/C

Add node.
https://localhost:44321/api/node/post

{
	"id": "newNode1"
	"name": "Z",
	"parentId": "7167b1a7-63be-4145-acbb-fc2d137ad81f",
	"children": [
	]
}


Update a node.
https://localhost:44321/api/node/put/7167b1a7-63be-4145-acbb-fc2d137ad81f
{
	"id": "7167b1a7-63be-4145-acbb-fc2d137ad81f",
	"name": "D - Has a new Parent H",
	"parentId": "72739fe6-7b19-4f2a-8559-c78a63ac76a1",
	"children": [
		"1410b0b0-45e2-4db1-ae27-15057881f41a",
		"fa85ee0f-f7e0-4449-9cf6-cf08b688b0ac"
	]
}

Delete a node and all its children
https://localhost:44321/api/node/delete/7167b1a7-63be-4145-acbb-fc2d137ad81f
