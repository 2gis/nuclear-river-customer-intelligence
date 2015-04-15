using System;

using NuClear.Messaging.API.Flows;

namespace NuClear.AdvancedSearch.Messaging.Metadata.Flows
{
    public sealed class Replicate2CustomerIntelligenceFlow : MessageFlowBase<Replicate2CustomerIntelligenceFlow>
    {
        public override Guid Id
        {
            get { return new Guid("9F2C5A2A-924C-485A-9790-9066631DB307"); }
        }

        public override string Description
        {
            get { return "������ ��� ������ ����������� �������� � ������� �������������� ���������� ��������� � ����� Customer Intelligence"; }
        }
    }
}