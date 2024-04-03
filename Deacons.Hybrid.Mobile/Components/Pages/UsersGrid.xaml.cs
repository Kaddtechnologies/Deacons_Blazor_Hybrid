using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Models;
using Microsoft.Extensions.Options;

namespace Deacons.Hybrid.Mobile.Components.Pages;

public partial class UsersGrid : ContentPage
{
	public List<TeamUserModel> users { get; set; }	
    private IUsersService user;
	public UsersGrid(IUsersService options)
	{
	   user = options;
		InitializeComponent();
		GetUsers();
	}

	public async Task<List<TeamUserModel>> GetUsers()
	{
		var userModel = await user.GetAll();
		this.users = (List<TeamUserModel>)userModel;
		return (List<TeamUserModel>)userModel;
	}
}