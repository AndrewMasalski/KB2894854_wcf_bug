KB2894854_wcf_bug
=================
Sample solution for checking if Windows update KB2894854 fail double encoding of WCF/OData requests.
On the broken environment .NET (WCF Data Services) infrastructure removes only single escaping.
On the environment without Windows update KB2894854 unit tests are passed successfully.