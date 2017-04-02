using System.Reflection;

namespace Express.Text {

    interface IMemberText {

        string ExtendedType { get; }

        string MemberName { get; }

        string TypeParameters { get; }
    }
}
