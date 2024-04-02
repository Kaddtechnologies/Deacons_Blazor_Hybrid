using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Models;
using Microsoft.Extensions.Options;

namespace Deacons.Hybrid.Mobile.Components.Pages;

public partial class UsersGrid : ContentPage
{
	List<User> users;	
    private IUsersService user;
	public UsersGrid(IUsersService options)
	{
	   user = options;
		InitializeComponent();
		GetUsers();
	}

	public async Task<List<User>> GetUsers()
	{
		var userModel = await user.GetAll();
		users = (List<User>)userModel;
		return (List<User>)userModel;
	}
}