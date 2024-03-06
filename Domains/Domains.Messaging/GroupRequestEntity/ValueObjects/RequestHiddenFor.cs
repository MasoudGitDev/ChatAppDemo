using Shared.Enums;

namespace Domains.Messaging.GroupRequestEntity.ValueObjects;
public record class RequestVisibility {
    public Visibility showToAdmins { get; private set; } = Visibility.Visible;
    public Visibility showToRequester { get; private set; } = Visibility.Visible;

    private RequestVisibility() {

    }

    public static RequestVisibility Default =>
        new() { showToAdmins = Visibility.Visible , showToRequester = Visibility.Visible };



    public void VisibleToAdmins(Visibility visibility) {
        showToAdmins = visibility;
    }
    public void VisibleToRequester(Visibility visibility) {
        showToRequester = visibility;
    }

}
